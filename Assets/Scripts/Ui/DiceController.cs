using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DiceController : MonoBehaviour, IPointerDownHandler
{

    public DiceUi diceUi;
    public Character character { get; set; }

    public int diceSpd { get; set; }

    public CardController choosingCard { get; set; }

    public CardData selectCard { get; set; }

    private int selectCardId;

    private DiceController defaultTarget;

    public DiceController target { get; set; }

    public List<DiceController> attacked = new List<DiceController>();
    public List<DiceController> onesideAttacked = new List<DiceController>();

    public bool clashing { get; set; }


    public void EndGame()
    {
        if (!character.IsEnemy) BattleManager.Instance.LibrarianDices.Remove(this);

        Destroy(gameObject);
    }

    private void EnemyAttackSelect()
    {
        // ���� ����
        int count = 0;
        bool isHave = false;
        while (!isHave)
        {

            if (character.stat.cardCount <= 0) break;

            int index = Random.Range(0, 9);

            if (character.stat.haveCards[index] == true)
            {

                if (character.stat.keyPage.cards[index].cardCost < character.stat.haveLight)
                {
                    isHave = true;

                    UseCard(character.stat.keyPage.cards[index]);

                    selectCard = character.stat.keyPage.cards[index];

                    character.stat.haveCards[index] = false;

                    // Ÿ���� �������� �Ұ�
                    int rand = BattleManager.Instance.LibrarianDices.Count;
                    defaultTarget = BattleManager.Instance.LibrarianDices[Random.Range(0, rand)];
                    target = defaultTarget;
                    target.onesideAttacked.Add(this);


                    diceUi.OneWayDice(target.diceUi);
                }

                else
                {
                    count++;
                }


            }

            // �ʹ� �����ð� ����� ī�带 �������� ���ϸ� �ൿ���� �ʵ���
            if (count > 50)
            {
                break;
            }

        }
    }


    public void SpeedResult()
    {
        diceUi.DiceSpdDecide();

        if (!character.stat.IsCharacterStaggered())
        {

            if (character.IsEnemy)
            {
                EnemyAttackSelect();
            }
            
        }
 
    }

    public void UseCard(CardData card)
    {

        selectCard = card;
        character.stat.cardCount--;
        for(int i = 1; i <= card.cardCost; i++)
        {
            character.stat.diceHandler.lights[character.stat.haveLight - i].DisableLight();

        }
        character.stat.haveLight -= card.cardCost;


        if (!character.IsEnemy)
        {
                      
            selectCardId = choosingCard.id;
            character.stat.haveCards[choosingCard.id] = false;
            choosingCard = null;
            
        }
    }


    public void Start()
    {
        if (!character.IsEnemy) BattleManager.Instance.LibrarianDices.Add(this);
    }


    public void NextAct()
    {
        gameObject.SetActive(true);


        selectCard = null;
        target = null;
        attacked.Clear();
        onesideAttacked.Clear();
        defaultTarget = null;
        clashing = false;

        if (!character.stat.IsCharacterStaggered()) diceUi.DiceSpdRolling();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClickDice();
        }
    }
    public void RightClickDice()
    {
        if (!character.IsEnemy)
        {
            if (selectCard != null)
            {

                target.attacked.Remove(this);
                target.onesideAttacked.Remove(this);

                if (target.target == this && target.clashing)
                {
                    target.SortAndResetTarget();
                }


                for(int i = 0; i < character.stat.keyPage.cards[selectCardId].cardCost; i++)
                {
                    character.stat.diceHandler.lights[character.stat.haveLight + i].EnableLight();
                }

                character.stat.haveLight += character.stat.keyPage.cards[selectCardId].cardCost;

                character.stat.haveCards[selectCardId] = true;
                character.stat.cardCount++;

                diceUi.UnEquipCard();

                target.diceUi.HideArrows();

                selectCard = null;
                clashing = false;
                target = null;
            }
        }
    }

    public void StartBattle()
    {
        gameObject.SetActive(false);

        if(selectCard != null)
        {
            BattleManager.Instance.battleLineUp.Add(this);
        }


    }

    public void SortAndResetTarget()
    {

        if(attacked.Count > 0)
        {
            target = attacked[0];
            attacked.Remove(target);

            clashing = true;
            target.clashing = true;
            diceUi.ClashingDice(target.diceUi);

        }


        else
        {

            target = defaultTarget;
            target.onesideAttacked.Add(this);
            clashing = false;

            diceUi.OneWayDice(target.diceUi);

        }


    }

    public void DecideOneSideAttack(DiceController target)
    {

        clashing = false;

        diceUi.OneWayDice(target.diceUi);


        // ���� �ӵ��� �� ���� ���¿��� �Ϲ������ �Ѵٸ�, ������ �� ��밡 ���������� �ڵ����� �� �ֻ����� ���� �ϵ��� �����.
        if (diceSpd > target.diceSpd)
        {
            target.attacked.Add(this);

        }

        // �ӵ��� �� ���� �ֻ����� �Ϲ�����̱� ������, �Ϲ���� ����Ʈ�� ����
        else
        {
            target.onesideAttacked.Add(this);
        }

    }
    public void SelectDice()
    {

        if (BattleManager.Instance.selectDice != null)
        {
            BattleManager.Instance.selectDice.diceUi.UnSelectDice();

            BattleManager.Instance.selectDice.diceUi.HideArrows();

            UiManager.Instance.cardHandler.gameObject.SetActive(false);

        }

        diceUi.SelectDice();

        if (character.IsEnemy)
        {
            SelectEnemyDice();
        }

        else
        {
            SelectTeamDice();
        }



    }

    private bool IsFastClashing(DiceController selectDice)
    {
        return selectDice.diceSpd > diceSpd && selectCard != null;
    }

    private bool IsTargetClashing(DiceController selectDice)
    {
        return defaultTarget == selectDice && target == selectDice;
    }

    private void SelectTeamDice()
    {
        BattleManager.Instance.selectDice = this;
        UiManager.Instance.teamInfoPanel.HoldInfo(this);

        if (!character.stat.IsCharacterStaggered())
        {
            UiManager.Instance.CardInit(character);
        }
    }

    private void SelectEnemyDice()
    {

        if (BattleManager.Instance.selectDice != null && BattleManager.Instance.selectDice.choosingCard != null)
        {
            CardSelectToEnemy();
        }

        else
        {
            BattleManager.Instance.selectDice = this;
            // showInfo
            UiManager.Instance.enemyInfoPanel.HoldInfo(this);

        }

    }

    private void CardSelectToEnemy()
    {
        DiceController selectDice = BattleManager.Instance.selectDice;

        selectDice.target = this;

        if (IsFastClashing(selectDice))
        {

            // ������ �� ���� �ֻ����� ���� �������Ƿ�, ������ ���� �ϴ� �ֻ����� �Ϲ�������� ó���ǰ� ����.
            if (clashing)
            {

                target.DecideOneSideAttack(this);

                selectDice.clashing = true;

                target = selectDice;

            }

            // �� ���� ���°� �ƴϹǷ� �ӵ��� ���� �ֻ����� ������ ����.
            else
            {

                selectDice.clashing = true;

                target.onesideAttacked.Remove(this);

                clashing = true;

                target = selectDice;

            }

            selectDice.diceUi.ClashingDice(diceUi);
        }

        else if (IsTargetClashing(selectDice))
        {
            selectDice.clashing = true;
            clashing = true;
            target.onesideAttacked.Remove(this);
            selectDice.diceUi.ClashingDice(diceUi);
        }

        else
        {
            selectDice.DecideOneSideAttack(this);
        }

        selectDice.character.stat.diceHandler.ResetLightAnim();
        selectDice.UseCard(selectDice.choosingCard.cardData);

        diceUi.UnSelectDice();

        UiManager.Instance.HideInfo();
    }

    
}
