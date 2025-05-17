using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using System;
using UnityEngine.Rendering;

[Serializable]
public class InBattleDice
{
    public InBattleDice(Dice dice)
    {
        this.dice = dice;
    }

    public Dice dice { get; private set; }

    public int resultValue { get; private set; } = 0;

    public bool diceRolled { get; private set; } = false;

    public void InitDiceValue(int diceValue)
    {
        resultValue = diceValue;

        diceRolled = true;
    }
}

[Serializable]
public class InBattleAction
{
    public List<InBattleDice> inBattleDices = new List<InBattleDice>();
    public int currentDiceIndex { get; private set; } = 0;

    public DiceController diceController { get; private set; }

    public CardData card { get; private set; }


    public void InitCounterAction(CounterAction action)
    {
        this.diceController = action.diceController;

        card = action.baseCard;

        foreach(InBattleDice dice in action.counterDices)
        {
            inBattleDices.Add(dice);
        }

    }

    public void InitDiceAction(DiceController diceController)
    {
        this.diceController = diceController;

        card = this.diceController.selectCard;

        foreach (Dice dice in card.dice)
        {
            InBattleDice inBattleDice = new InBattleDice(dice);

            inBattleDices.Add(inBattleDice);
        }
    }

    public void InitDice(Dice[] dices)
    {
        foreach(Dice dice in dices)
        {
            InBattleDice inBattleDice = new InBattleDice(dice);

            inBattleDices.Add(inBattleDice);
        }
    }

    public void InitCounterDice(InBattleDice dice)
    {
        inBattleDices.Add(dice);
    }

    public InBattleDice GetCurrentDice()
    {
        if (inBattleDices.Count > currentDiceIndex) return inBattleDices[currentDiceIndex];

        else return null;
    }

    public InBattleDice GetPastDice()
    {
        if (inBattleDices.Count > currentDiceIndex - 1 && currentDiceIndex - 1 >= 0)
        {
            return inBattleDices[currentDiceIndex - 1];
        }

        else return null;
    }

    public void DiceRolled(int diceValue)
    {
        inBattleDices[currentDiceIndex].InitDiceValue(diceValue);

        currentDiceIndex += 1;
    }

    public void RefreshCard()
    {
        inBattleDices.Clear();

        diceController = null;

        card = null;

        currentDiceIndex = 0;
    }

    public void ReUseDice()
    {
        currentDiceIndex -= 1;
    }

    public InBattleDice GetUnUsedDice()
    {
        InBattleDice currentDice = GetCurrentDice();

        currentDiceIndex += 1;

        if (currentDice.diceRolled)
        {
            return null;
        }

        else
        {
            return currentDice;
        }
    }
}
[Serializable]
public class CounterAction
{
    public CardData baseCard { get; private set; }

    public List<InBattleDice> counterDices { get; private set; } = new List<InBattleDice>();

    public DiceController diceController { get; set; }

    public void AddBaseCard(CardData card)
    {
        if(baseCard == null) baseCard = card;
    }

    public void AddCounterDice(InBattleDice dice)
    {
        counterDices.Add(dice);
    }

    public void RefreshAction()
    {
        baseCard = null;

        counterDices.Clear();

        diceController = null;
    }

}

public class Character : MonoBehaviour
{
    [SerializeField]
    private bool isEnemy;

    public bool IsEnemy { get { return isEnemy; } }

    public int id;

    public InBattleAction battleAction;

    public CounterAction counterAction;

    public int DiceRoll()
    {
        Dice dice = battleAction.GetCurrentDice().dice;

        int diceValue = dice.DiceRoll();

        diceValue += behaviour.DiceBonusCalculate(dice.diceType);

        ui.Rencounter.DiceResult(diceValue);

        battleAction.DiceRolled(diceValue);

        return diceValue;
    }

    public void AddCounterDice()
    {
        InBattleDice unusedDice = battleAction.GetUnUsedDice();

        if (unusedDice != null)
        {


            CardData card = battleAction.diceController.selectCard;

            counterAction.AddBaseCard(card);
            counterAction.AddCounterDice(unusedDice);



            CounterDiceUi counterDiceUi = ui.CounterDicePreview.GetDice();
            counterDiceUi.InitDice(unusedDice);
        }

    }



    public void CounterActionInit(DiceController actionDice, Character target)
    {

        counterAction.diceController = actionDice;


        battleAction.InitCounterAction(counterAction);


        ui.Rencounter.SetRencounter(battleAction, target);

        ui.CounterDicePreview.UsedCounterDice();
    }

    public void ActionInit(DiceController actionDice, Character target)
    {
        CardData card = actionDice.selectCard;

        card.CardUseEffect(this);

        battleAction.InitDiceAction(actionDice);

        ui.Rencounter.SetRencounter(battleAction, target);



        ui.CounterDicePreview.HideCounterDice();

    }

    [Header("Modules")]
    public CharacterMove move;

    public CharacterBehaviour behaviour;

    public CharacterStat stat;

    public CharacterUi ui;

    public ParticleSystem teleportParticle;

    [Header("Etc")]
    [SerializeField]
    private RenderTexture rt;
    public RenderTexture renderTexture { get { return rt; } }

    private SortingGroup sortingGroup;

    public event Action onBattleLayer;

    public event Action onDefaultLayer;

    private void InitCharacterInfo(CharacterBaseInfo baseInfo)
    {
        if (baseInfo != null)
        {
            move.InitCharacterPos(baseInfo);

            stat.InitBaseInfo(baseInfo);

            ui.Init(this);

            InitEvent();
        }

    }

    private void LoadCharacterInfo()
    {
        if (isEnemy)
        {
            List<CharacterBaseInfo> enemyBaseInfoList = ResourceManager.Instance.selectStageEnemy;

            if(id < enemyBaseInfoList.Count)
            {
                InitCharacterInfo(enemyBaseInfoList[id]);
                BattleManager.Instance.enemyList.Add(this);
            }

            else
            {
                gameObject.SetActive(false);
            }


        }

        else
        {
            List<CharacterBaseInfo> teamBaseInfoList = ResourceManager.Instance.allowTeamCharacter;

            if (id < teamBaseInfoList.Count)
            {
                InitCharacterInfo(teamBaseInfoList[id]);
                BattleManager.Instance.teamList.Add(this);
            }

            else
            {
                gameObject.SetActive(false);
            }

        }
    }


    public void BattleAction(Character target)
    {
        stat.StateChange(CharacterState.Move);

        ui.Rencounter.NxtRencounter();

        move.MoveToTarget(target.transform);
    }
    
    public void EndBattleAction()
    {
        ui.Rencounter.EndCard();

        ui.CounterDicePreview.DisplayCounterDice();

        ChangeToDefaultLayer();

        stat.StateChange(CharacterState.Default);

        battleAction.RefreshCard();
    }

    private void Awake()
    {
        stat = GetComponent<CharacterStat>();
        behaviour = GetComponent<CharacterBehaviour>();
        ui = GetComponent<CharacterUi>();
        move = GetComponent<CharacterMove>();
        sortingGroup = GetComponent<SortingGroup>();


    }

    public void ChangeToBattleLayer()
    {
        sortingGroup.sortingLayerName = "BattleLayer";
        onBattleLayer?.Invoke();
    }

    public void ChangeToDefaultLayer()
    {
        sortingGroup.sortingLayerName = "Default";
        onDefaultLayer?.Invoke();
    }

    private void Start()
    {
        LoadCharacterInfo();

        teleportParticle.transform.position = transform.position + new Vector3(0, 0.8f, 0);
    }

    private void InitEvent()
    {
        BattleManager.Instance.onEndAct += EndAct;

        BattleManager.Instance.onNextAct += NextAct;

        stat.onDeadCharacter += DeadCharacter;
    }

    public void WinClash(Character target)
    {
        InBattleDice inBattleDice = battleAction.GetPastDice();

        stat.StateChange(inBattleDice.dice.characterMotion);


        if (inBattleDice.dice.diceType is DiceType.Evade)
        {
            // EVADE MOVE
            move.EvadeMove(target.transform);

            if(target.battleAction.GetPastDice().dice.diceType is DiceType.Evade or DiceType.Block)
            {
                return;
            }

            battleAction.ReUseDice();
        }

        behaviour.WinClash(target, inBattleDice);
    }

    public void OneSideAttack(Character target)
    {
        InBattleDice inBattleDice = battleAction.GetPastDice();

        stat.StateChange(inBattleDice.dice.characterMotion);

        behaviour.OneSideAttack(target, inBattleDice);
        //behaviour.OneSideAttack(target, inBattleDice.dice, inBattleDice.resultValue);

        move.AttackMove(target.transform);

    }


    public void LoseClash()
    {
        stat.StateChange(CharacterState.Damaged);

        behaviour.LoseClash();
        ui.Rencounter.LoseClash();
    }

    public void DrawClash(Character target)
    {
        InBattleDice inBattleDice = battleAction.GetPastDice();

        stat.StateChange(inBattleDice.dice.characterMotion);

        behaviour.DrawClash(inBattleDice);

        move.Knockback(target.transform);
    }

    public void TakeAttack(Character atker, DiceType type, int value)
    {

        stat.StateChange(CharacterState.Damaged);

        move.Knockback(atker.transform);

        stat.TakeAttack(type, value);

    }

    public void AttackFail(Character atker)
    {
        InBattleDice inBattleDice = battleAction.GetPastDice();

        stat.StateChange(inBattleDice.dice.characterMotion);

        behaviour.AttackEffect(inBattleDice.dice);

        move.Knockback(atker.transform);

        stat.StateChange(CharacterState.Damaged);
    }


    private void NextAct()
    {
        teleportParticle.Stop();
    }

    private void EndAct()
    {
        counterAction.RefreshAction();

        ui.CounterDicePreview.UsedCounterDice();


        teleportParticle.Play();

    }

    public void DeadCharacter()
    {
        BattleManager.Instance.onEndAct -= EndAct;
        BattleManager.Instance.onNextAct -= NextAct;

        StartCoroutine(DeadCharacterCoroutine());
    }

    private IEnumerator DeadCharacterCoroutine()
    {
        // »ç¸Á
        float particleTime = 0.5f;
        float disableTime = 0.3f;

        if (isEnemy) BattleManager.Instance.enemyList.Remove(this);
        else BattleManager.Instance.teamList.Remove(this);

        teleportParticle.Play();

        yield return new WaitForSeconds(particleTime);

        teleportParticle.Stop();

        yield return new WaitForSeconds(disableTime);

        gameObject.SetActive(false);
    }


}
