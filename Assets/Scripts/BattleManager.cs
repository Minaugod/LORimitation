using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EnumTypes;
using UnityEngine.SceneManagement;
using System;
public class BattleManager : MonoBehaviour
{

    public event Action onSpdDecide;

    public event Action onStartBattle;

    public event Action onEndAct;

    public event Action onNextAct;

    public event Action onEndGame;

    public event Action onRightClick;

    [Header("Battle Info")]
    public int act = 0;

    public DiceController selectDice;

    public List<Character> enemyList = new List<Character>();

    public List<Character> teamList = new List<Character>();

    public List<DiceController> battleLineUp = new List<DiceController>();

    public List<DiceController> LibrarianDices = new List<DiceController>();

    [Header("etc")]
    public BattleBackground battleBackground;

    [SerializeField]
    private float battleDelay;

    [Header("Game State")]
    private bool spdDecided;

    private bool isBattle;


    private void BattleInit()
    {
        act = 1;
        UiManager.Instance.gameStateEffect.ActChange(act);
    }


    private void StartBattle()
    {
        selectDice = null;

        // 배틀 순서 정렬
        onStartBattle.Invoke();
        UiManager.Instance.HideInfo();

        battleLineUp.Sort((a, b) => b.diceSpd.CompareTo(a.diceSpd));


        StartCoroutine(BattleCoroutine());

    }




    IEnumerator BattleCoroutine()
    {
        if (battleLineUp.Count > 0)
        {
            battleBackground.ChangeToBattleBridge();

            for (int i = 0; i < battleLineUp.Count; i++)
            {
                yield return BattleAction(battleLineUp[i]);
            }

        }


        EndAct();
        battleBackground.ChangeToNormalBridge();
        yield break;
    }

    IEnumerator CheckClashingMovable(DiceController A_Dice, DiceController B_Dice)
    {
        battleLineUp.Remove(B_Dice);

        if (CheckStaggered(A_Dice))
        {

            if (CheckStaggered(B_Dice)) yield break;

            DiceActionInit(B_Dice, A_Dice);
            yield return OneSideAct(B_Dice, A_Dice);
        }

        else
        {
            if (CheckStaggered(B_Dice))
            {
                DiceActionInit(A_Dice, B_Dice);
                yield return OneSideAct(A_Dice, B_Dice);
            }

            else
            {
                
                DiceActionInit(A_Dice, B_Dice);
                DiceActionInit(B_Dice, A_Dice);
                

                yield return ClashingAct(A_Dice, B_Dice);

            }
        }

    }

    IEnumerator CheckOneSideCounter(DiceController atkDice, DiceController targetDice)
    {
        if (!CheckStaggered(atkDice))
        {
            if(targetDice.character.counterAction.counterDices.Count > 0)
            {

                DiceActionInit(atkDice, targetDice);
                CounterActionInit(targetDice, atkDice);

                yield return ClashingAct(atkDice, targetDice);
            }

            else
            {
                DiceActionInit(atkDice, targetDice);
                yield return OneSideAct(atkDice, targetDice);
            }
        }
    }

    private void DiceActionInit(DiceController atkDice, DiceController targetDice)
    {
        Character atker = atkDice.character;
        Character target = targetDice.character;

        atker.ActionInit(atkDice, target);
    }

    private void CounterActionInit(DiceController atkedDice, DiceController atkDice)
    {
        Character atkedCharacter = atkedDice.character;
        Character atker = atkDice.character;

        atkedCharacter.CounterActionInit(atkedDice, atker);
    }

    private bool CheckAlive(DiceController dice) { return dice.character.stat.IsCharacterAlive(); }
    private bool CheckStaggered(DiceController dice) { return dice.character.stat.IsCharacterStaggered(); }
    IEnumerator BattleAction(DiceController diceA)
    {

        

        DiceController targetDice = diceA.target;

        if (targetDice == null || !CheckAlive(targetDice))
        {
            battleBackground.StopCameraFollow();
            yield break;
        }


        if (CheckAlive(diceA))
        {
            battleBackground.BattleCameraFollow(diceA.character.transform, targetDice.character.transform);

            diceA.character.ChangeToBattleLayer();
            targetDice.character.ChangeToBattleLayer();


            if (diceA.clashing) yield return CheckClashingMovable(diceA, targetDice);

            else yield return CheckOneSideCounter(diceA, targetDice);


        }

        yield return new WaitForSeconds(battleDelay);

        diceA.character.EndBattleAction();
        targetDice.character.EndBattleAction();

        // End
        battleBackground.StopCameraFollow();

        yield return null;
    }


    public void BattleSlowMotion()
    {
        battleBackground.BattleSlowMotion();
    }


    IEnumerator ClashingAct(DiceController A_Dice, DiceController B_Dice)
    {

        Character characterA = A_Dice.character;
        Character characterB = B_Dice.character;


        //characterA.ActionInit(A_Dice, characterB);
        //characterB.ActionInit(B_Dice, characterA);

        while (characterA.battleAction.GetCurrentDice() != null || characterB.battleAction.GetCurrentDice() != null)
        {
            InBattleDice A_BattleDice = characterA.battleAction.GetCurrentDice();
            InBattleDice B_BattleDice = characterB.battleAction.GetCurrentDice();

            if(A_BattleDice != null)
            {
                characterA.BattleAction(characterB);
            }

            if(B_BattleDice != null)
            {
                characterB.BattleAction(characterA);
            }

            yield return new WaitUntil(() => !characterA.move.IsMoving && !characterB.move.IsMoving);

            yield return new WaitForSeconds(0.1f);


            yield return ClashingCheck(characterA, characterB);



            if (!CheckAlive(A_Dice) || !CheckAlive(B_Dice)) break;

            yield return null;
        }


        yield return null;
    }

    IEnumerator OneSideAct(DiceController atkerDice, DiceController targetDice)
    {
        Character characterA = atkerDice.character;
        Character characterB = targetDice.character;

        //characterA.ActionInit(atkerDice, characterB);

        while (characterA.battleAction.GetCurrentDice() != null)
        {

            characterA.BattleAction(characterB);

            yield return new WaitUntil(() => !characterA.move.IsMoving);

            yield return new WaitForSeconds(0.1f);


            yield return OneSideAttack(characterA, characterB);

            if (!CheckAlive(atkerDice) || !CheckAlive(targetDice)) break;

            yield return null;
        }

        yield return null;
    }


    IEnumerator ClashingCheck(Character characterA, Character characterB)
    {
        InBattleDice diceA = characterA.battleAction.GetCurrentDice();
        InBattleDice diceB = characterB.battleAction.GetCurrentDice();

        if (diceA != null && diceB == null)
        {
            yield return OneSideAttack(characterA, characterB);
        }

        else if (diceB != null && diceA == null)
        {
            yield return OneSideAttack(characterB, characterA);
        }

        else if (diceA != null && diceB != null)
        {
            yield return Clashing(characterA, characterB);
        }

        else
        {
            yield break;
        }
    }

    IEnumerator Clashing(Character characterA, Character characterB)
    {

        int A_DiceValue = characterA.DiceRoll();
        int B_DiceValue = characterB.DiceRoll();

        yield return new WaitForSeconds(battleDelay);

        if (A_DiceValue > B_DiceValue)
        {

            characterA.WinClash(characterB);
            characterB.LoseClash();


            battleBackground.BattleCameraShake(A_DiceValue);


        }

        else if (B_DiceValue > A_DiceValue)
        {

            characterB.WinClash(characterA);
            characterA.LoseClash();


            battleBackground.BattleCameraShake(B_DiceValue);
        }

        else
        {
            //DRAW
            characterA.DrawClash(characterB);
            characterB.DrawClash(characterA);
        }


        yield return new WaitForSeconds(battleDelay);


        yield return null;
    }


    IEnumerator OneSideAttack(Character atker, Character target)
    {
        Dice cardDice = atker.battleAction.GetCurrentDice().dice;

        if (cardDice.diceType is DiceType.Block or DiceType.Evade)
        {
            atker.AddCounterDice();

            yield break;
        }

        int diceValue = atker.DiceRoll();

        yield return new WaitForSeconds(battleDelay);

        atker.OneSideAttack(target);

        battleBackground.BattleCameraShake(diceValue);

        yield return new WaitForSeconds(battleDelay);


    }

    /*
    private bool IsCanNextAction(int maxActionCount, int nowActionCount)
    {
        return maxActionCount > nowActionCount;
    }

    private CardData SetCard(DiceController dice)
    {

        CardData card = dice.selectCard;

        card.CardUseEffect(dice.character);

        return card;

    }

    
    private void NextLineupAction(Character atker, Character target, int diceIndex)
    {
        atker.InitBattleAction(target, diceIndex);
    }

    private void SetRencounter(DiceController dice, DiceController targetDice)
    {

        CardData card = dice.selectCard;

        if (dice.character.transform.position.x - targetDice.character.transform.position.x < 0)
        {
            dice.character.ui.Rencounter.SetLeftRencounter(card);
        }

        else
        {
            dice.character.ui.Rencounter.SetRightRencounter(card);
        }



    }
    
    IEnumerator ClashingSchedule(DiceController A_Dice, DiceController B_Dice)
    {

        CardData A_Card = SetCard(A_Dice);
        CardData B_Card = SetCard(B_Dice);

        SetRencounter(A_Dice, B_Dice);
        SetRencounter(B_Dice, A_Dice);

        List<Dice> A_CardDiceList = CreateCardDiceList(A_Card.dice);
        List<Dice> B_CardDiceList = CreateCardDiceList(B_Card.dice);


        int A_DiceIndex = 0, B_DiceIndex = 0;
        while (IsCanNextAction(A_CardDiceList.Count, A_DiceIndex) || B_CardDiceList.Count > B_DiceIndex)
        {

            if(A_CardDiceList.Count > A_DiceIndex)
            {
                NextLineupAction(A_Dice.character, B_Dice.character, A_DiceIndex);
                A_Dice.character.inBattleDice = A_CardDiceList[A_DiceIndex];
            }

            if (B_CardDiceList.Count > B_DiceIndex)
            {
                NextLineupAction(B_Dice.character, A_Dice.character, B_DiceIndex);
                B_Dice.character.inBattleDice = B_CardDiceList[B_DiceIndex];
            }

            yield return new WaitUntil(() => !A_Dice.character.move.IsMoving && !A_Dice.character.move.IsMoving);

            // 이동 후
            yield return new WaitForSeconds(0.1f);

            yield return CheckIsClashing(A_Dice, B_Dice);

            if (!CheckAlive(A_Dice) || !CheckAlive(B_Dice)) break;

            if (CheckStaggered(A_Dice)) A_CardDiceList.Clear();
            if (CheckStaggered(B_Dice)) B_CardDiceList.Clear();

            if (CheckReUseDice(A_Dice, B_Dice)) A_DiceIndex--;
            if (CheckReUseDice(B_Dice, A_Dice)) B_DiceIndex--;

            A_DiceIndex++;
            B_DiceIndex++;

            A_Dice.character.inBattleDice = null;
            B_Dice.character.inBattleDice = null;

        }


        yield return null;
    }

    

    private bool CheckReUseDice(DiceController A_Dice, DiceController B_Dice)
    {
        Dice A_CardDice = A_Dice.character.inBattleDice;
        Dice B_CardDice = B_Dice.character.inBattleDice;

        int A_DiceValue = A_Dice.character.inBattleDiceValue;
        int B_DiceValue = B_Dice.character.inBattleDiceValue;

        if (A_CardDice != null && B_CardDice != null)
        {
            if (A_CardDice.diceType is DiceType.Evade && A_DiceValue > B_DiceValue) return true;

            else return false;
        }

        else
        {
            return false;
        }

    }

    /*

    IEnumerator CheckIsClashing(DiceController A_Dice, DiceController B_Dice)
    {

        Dice cardDiceA = A_Dice.character.inBattleDice;
        Dice cardDiceB = B_Dice.character.inBattleDice;

        if (cardDiceA != null && cardDiceB == null)
        {
            yield return OneSideAttackAction(A_Dice, B_Dice);
        }

        else if (cardDiceB != null && cardDiceA == null)
        {
            yield return OneSideAttackAction(B_Dice, A_Dice);
        }

        else if (cardDiceA != null && cardDiceB != null)
        {
            yield return ClashingAction(A_Dice, B_Dice);
        }

        else
        {
            yield break;
        }

    }


    IEnumerator ClashingAction(DiceController A_Dice, DiceController B_Dice)
    {
        Dice cardDiceA = A_Dice.character.inBattleDice;
        Dice cardDiceB = B_Dice.character.inBattleDice;

        int diceAValue, diceBValue;
        diceAValue = CalcDiceValue(cardDiceA, A_Dice.character);
        diceBValue = CalcDiceValue(cardDiceB, B_Dice.character);

        yield return new WaitForSeconds(battleDelay);

        if (diceAValue > diceBValue)
        {

            A_Dice.character.WinClash(B_Dice.character);
            B_Dice.character.LoseClash();


            battleBackground.BattleCameraShake(diceAValue);


        }

        else if (diceBValue > diceAValue)
        {

            B_Dice.character.WinClash(A_Dice.character);
            A_Dice.character.LoseClash();


            battleBackground.BattleCameraShake(diceBValue);
        }

        else
        {
            //DRAW
            A_Dice.character.DrawClash(B_Dice.character);
            B_Dice.character.DrawClash(A_Dice.character);
        }


        yield return new WaitForSeconds(battleDelay);
    }

    IEnumerator OneSideAttackAction(DiceController atkerDice, DiceController targetDice)
    {
        Dice cardDice = atkerDice.character.inBattleDice;

        if (cardDice.diceType is DiceType.Block or DiceType.Evade)
        {


            yield break;
        }

        int diceValue = CalcDiceValue(cardDice, atkerDice.character);

        yield return new WaitForSeconds(battleDelay);

        atkerDice.character.OneSideAttack(targetDice.character);

        battleBackground.BattleCameraShake(diceValue);

        yield return new WaitForSeconds(battleDelay);


    }

    private int CalcDiceValue(Dice cardDice, Character character)
    {
        int diceValue = cardDice.DiceRoll();

        diceValue += character.behaviour.DiceBonusCalculate(cardDice.diceType);
        character.ui.Rencounter.DiceResult(diceValue);

        character.inBattleDiceValue = diceValue;

        return diceValue;
    }

    IEnumerator OneSideAttackSchedule(DiceController atkerDice, DiceController targetDice)
    {
        CardData card = SetCard(atkerDice);

        List<Dice> cardDiceList = CreateCardDiceList(card.dice);

        SetRencounter(atkerDice, targetDice);

        int A_DiceIndex = 0;

        while (IsCanNextAction(cardDiceList.Count, A_DiceIndex))
        {

            NextLineupAction(atkerDice.character, targetDice.character, A_DiceIndex);

            atkerDice.character.inBattleDice = cardDiceList[A_DiceIndex];

            yield return new WaitUntil(() => !atkerDice.character.move.IsMoving);


            yield return OneSideAttackAction(atkerDice, targetDice);

            if (!CheckAlive(atkerDice) || !CheckAlive(targetDice)) break;

            A_DiceIndex++;

        }

        yield return null;
    }

    

    private List<Dice> CreateCardDiceList(Dice[] diceArray)
    {
        List<Dice> diceList = new List<Dice>();
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceList.Add(diceArray[i]);
        }
        return diceList;
    }

    */

    private void EndAct()
    {
        onEndAct.Invoke();

        Invoke("NextAct", 1.1f);

    }

    void NextAct()
    {
        isBattle = false;

        if (enemyList.Count == 0)
        {
            // ENd Game
            UiManager.Instance.gameStateEffect.DisplayResult(GameResult.Victory);
            StartCoroutine(BackToLobby());
        }

        else if (teamList.Count == 0)
        {
            UiManager.Instance.gameStateEffect.DisplayResult(GameResult.Defeat);
            StartCoroutine(BackToLobby());
        }

        else
        {
            act++;
            UiManager.Instance.gameStateEffect.ActChange(act);

            battleLineUp.Clear();

        }

        onNextAct?.Invoke();


    }

    private IEnumerator BackToLobby()
    {
        yield return new WaitForSeconds(1f);

        onEndGame.Invoke();

        UiManager.Instance.gameStateEffect.ScreenFadeOut();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainScene");

        yield return null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            onRightClick.Invoke();
            if(selectDice != null)
            {
                if (selectDice.choosingCard == null)
                {
                    selectDice = null;
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isBattle)
            {
                if (spdDecided)
                {
   
                    spdDecided = false;
                    isBattle = true;
                    StartBattle();

                }

                else
                {
                    spdDecided = true;
                    onSpdDecide.Invoke();
                }

            }


        }

    }

    private static BattleManager instance;

    public static BattleManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BattleInit();
    }
}
