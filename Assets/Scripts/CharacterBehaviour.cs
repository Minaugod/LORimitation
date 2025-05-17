using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EnumTypes;
using System;
public class CharacterBehaviour : MonoBehaviour
{

    private Character character;

    private AttackEffect attackEffect;


    public event Action<Character, Character> onWinClash;

    public event Action<Character> onLoseClash;

    public event Action onMaxValue;

    public event Action onMinValue;

    public event Action<Character, Character> onAttackHit;

    public event Action<Character, DiceType> onDiceRoll;

    public event Action<Character> onNextAct;


    private void Awake()
    {
        character = GetComponent<Character>();

        attackEffect = GetComponentInChildren<AttackEffect>();
    }

    private void Start()
    {
        BattleManager.Instance.onNextAct += NextAct;
        character.stat.onDeadCharacter += DeadCharacter;
    }


    public int DiceBonusCalculate(DiceType diceType)
    {

        int diceValue;

        character.stat.ResetDiceBonusValue();

        onDiceRoll?.Invoke(character, diceType);

        diceValue = character.stat.diceBonusValueDic[diceType];


        return diceValue;
    }

    public void OneSideAttack(Character target, InBattleDice inBattleDice)
    {

        Dice dice = inBattleDice.dice;
        int value = inBattleDice.resultValue;

        AttackEffect(dice);

        target.TakeAttack(character, dice.diceType, value);


        if (dice.IsMaxValue(character, value))
        {
            onMaxValue?.Invoke();

            UiManager.Instance.DisplayOnMaximumDamage(target.transform);
            BattleManager.Instance.BattleSlowMotion();

        }

        else if (dice.IsMinValue(character, value))
        {
            onMinValue?.Invoke();

        }


        dice.ApplyEffect(character, target, DiceUseType.OnHit);

        //
        onAttackHit.Invoke(character, target);

    }

    public void WinClash(Character target, InBattleDice inBattleDice)
    {
        Dice dice = inBattleDice.dice;
        int value = inBattleDice.resultValue;

        dice.ApplyEffect(character, target, DiceUseType.OnWinClash);

        AttackEffect(dice);


        if (dice.diceType is DiceType.Evade or DiceType.Block)
        {
            target.AttackFail(character);
        }

        else
        {
            target.TakeAttack(character, dice.diceType, value);

            //OnAttackHit.Invoke(target);

            dice.ApplyEffect(character, target, DiceUseType.OnHit);
        }


        onWinClash?.Invoke(character, target);

        if (dice.IsMaxValue(character, value))
        {
            // 최대값!
            onMaxValue?.Invoke();

        }

        else if (dice.IsMinValue(character, value))
        {
            // 최소값!
            onMinValue?.Invoke();

        }

    }


    public void DrawClash(InBattleDice inBattleDice)
    {
        AttackEffect(inBattleDice.dice);
    }

    public void LoseClash()
    {
        onLoseClash?.Invoke(character);
    }

    public void NextAct()
    {
        onNextAct?.Invoke(character);
    }

    public void DeadCharacter()
    {
        BattleManager.Instance.onNextAct -= NextAct;
    }

    public void AttackEffect(Dice atkDice)
    {
        attackEffect.DisplayEffect(atkDice.atkEffect);
    }


}
