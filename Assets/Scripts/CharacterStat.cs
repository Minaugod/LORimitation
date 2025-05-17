using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EnumTypes;
using System;
public class CharacterStat : MonoBehaviour
{
    private Character character;

    public string characterName;

    public KeyPage keyPage;

    [SerializeField]
    private PassiveHandler passiveHandler;

    public DiceHandler diceHandler;

    public Dictionary<DiceType, int> diceBonusValueDic = new Dictionary<DiceType, int>()
    {
        { DiceType.Slash, 0 },
        { DiceType.Pierce, 0 },
        { DiceType.Blunt, 0 },
        { DiceType.Block, 0 },
        { DiceType.Evade, 0 }
    };

    public CharacterState state { get { return appearance.state; } }

    private Appearance appearance;

    public int haveLight;

    private int hp;

    public int Hp
    {
        get { return hp; }

        set
        {
            if (value > keyPage.page.hp) hp = keyPage.page.hp;

            else
            {
                hp = value;
            }


            if (hp <= 0)
            {
                hp = 0;
                appearance.HideCharacter();
                onDeadCharacter?.Invoke();
            }

            onHpChanged?.Invoke();

        }
    }

    public event Action onHpChanged;

    public event Action onDeadCharacter;

    private int stagger;

    public int Stagger
    {
        get { return stagger; }

        set
        {
            stagger = value;

            if (stagger <= 0)
            {
                stagger = 0;

                if (staggeredDuration <= 0)
                {
                    staggeredDuration = 2;

                    BattleManager.Instance.BattleSlowMotion();
                    UiManager.Instance.DisplayOnStaggered(transform);

                    character.battleAction.RefreshCard();

                    onStaggered.Invoke();
                }

            }

            onStaggerChanged?.Invoke();
        }
    }

    public event Action onStaggerChanged;

    private int staggeredDuration;

    public int cardCount;

    public bool[] haveCards = new bool[9];

    public Buff buff = new Buff();

    public Buff nextTurnBuff = new Buff();



    [Header("Events")]
    public UnityEvent onStaggered = new UnityEvent();

    public UnityEvent unStaggered = new UnityEvent();

    public UnityEvent ApplyBuff = new UnityEvent();

    public event Action<BuffUi> onAddBuff;

    public void ResetDiceBonusValue()
    {
        diceBonusValueDic[DiceType.Slash] = 0;
        diceBonusValueDic[DiceType.Pierce] = 0;
        diceBonusValueDic[DiceType.Blunt] = 0;
        diceBonusValueDic[DiceType.Block] = 0;
        diceBonusValueDic[DiceType.Evade] = 0;
    }

    public void AddBuff(BuffUi buff)
    {
        onAddBuff?.Invoke(buff);
    }


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void InitBaseInfo(CharacterBaseInfo baseInfo)
    {
        keyPage = baseInfo.keyPage;

        characterName = baseInfo.name;

        Hp = keyPage.page.hp;
        Stagger = keyPage.page.staggerResist;

        AddAppearance();

        DrawCard();
        DrawCard();
        DrawCard();

        diceHandler = UiManager.Instance.EquipDiceForCharacter(character);

        passiveHandler.EquipPassiveEffects(keyPage.passiveEffects);

        StateChange(CharacterState.Default);

        BattleManager.Instance.onEndAct += EndAct;
        BattleManager.Instance.onNextAct += NextAct;


    }

    private void AddAppearance()
    {
        appearance = Instantiate(keyPage.page.appearance).GetComponent<Appearance>();
        appearance.transform.SetParent(transform);

        Vector3 appearancePos = transform.position + new Vector3(0f, 0.8f, 0f);
        appearance.transform.position = appearancePos;

        appearance.transform.localEulerAngles = Vector3.zero;
    }

    public void TakeJustDamage(int value)
    {
        Hp -= value;

        UiManager.Instance.DisplayDmg(transform, value);

    }
    public void TakeAttack(DiceType type, int value)
    {

        appearance.TakeAttack();

        float dmgResist;
        float staggerDmgResist;

        if (IsCharacterStaggered())
        {
            dmgResist = 2f;
            staggerDmgResist = 2f;
        }

        else
        {
            dmgResist = CalcDamageRate(type).dmg;
            staggerDmgResist = CalcDamageRate(type).staggerDmg;
        }


        int dmg = Mathf.RoundToInt(value * dmgResist);
        int staggerDmg = Mathf.RoundToInt(value * staggerDmgResist);

        dmg -= buff.protection;

        Hp -= dmg;
        Stagger -= staggerDmg;

        UiManager.Instance.DisplayAttackDmg(character, type, dmg);
        UiManager.Instance.DisplayAttackStagger(character, type, staggerDmg);
    }

    private (float dmg, float staggerDmg) CalcDamageRate(DiceType type)
    {

        float dmgRate = 0f;
        float staggerRate = 0f;

        switch (type)
        {
            case DiceType.Slash:

                dmgRate = ResourceManager.Instance.resistDmgValue[keyPage.page.slashDmgResist];
                staggerRate = ResourceManager.Instance.resistDmgValue[keyPage.page.slashStaggerResist];

                break;

            case DiceType.Pierce:

                dmgRate = ResourceManager.Instance.resistDmgValue[keyPage.page.pierceDmgResist];
                staggerRate = ResourceManager.Instance.resistDmgValue[keyPage.page.pierceStaggerResist];

                break;

            case DiceType.Blunt:

                dmgRate = ResourceManager.Instance.resistDmgValue[keyPage.page.bluntDmgResist];
                staggerRate = ResourceManager.Instance.resistDmgValue[keyPage.page.bluntStaggerResist];

                break;

        }

        return (dmgRate, staggerRate);

    }

    private void EndAct()
    {
        staggeredDuration--;

        DrawCard();

        ApplyBuffs();

        diceHandler.RechargeLight();

        appearance.HideCharacter();
    }

    public void RechargeAllLight()
    {
        diceHandler.RechargeAllLight();
    }

    private void NextAct()
    {
        StateChange(CharacterState.Default);

        if (IsCharacterStaggered() && staggeredDuration == 0)
        {
            stagger = keyPage.page.staggerResist;

            unStaggered.Invoke();
        }
    }

    public void StateChange(CharacterState state) { appearance.StateChange(state); }

    private void ApplyBuffs()
    {
        buff = nextTurnBuff;
        nextTurnBuff = new Buff();
        ApplyBuff.Invoke();
    }


    private void DrawCard()
    {
        if (cardCount < 9)
        {
            bool isDuplicate = true;

            while (isDuplicate)
            {
                int index = UnityEngine.Random.Range(0, 9);


                if (haveCards[index] == false)
                {
                    haveCards[index] = true;
                    isDuplicate = false;
                }

            }

            cardCount++;
        }
    }


    public int DiceSpdDecide()
    {
        int spd;
        spd = UnityEngine.Random.Range(keyPage.page.spdDiceMin, keyPage.page.spdDiceMax + 1);
        spd += buff.haste;
        return spd;
    }



    public bool IsCharacterAlive() { return Hp > 0; }

    public bool IsCharacterStaggered() { return Stagger <= 0; }

}
