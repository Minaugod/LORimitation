using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
public class DiceUi : MonoBehaviour
{
    private DiceController dice;

    [SerializeField]
    private Animator diceGlowAnim;

    [SerializeField]
    private TMP_Text decidedSpd;

    [SerializeField]
    private TMP_Text rollSpd;


    private TargetArrow targetArrow;

    [SerializeField]
    private GameObject diceGlow;

    [SerializeField]
    private GameObject diceRoulette;

    [SerializeField]
    private GameObject diceSelect;

    [SerializeField]
    private Button diceBtn;

    [SerializeField]
    private Image diceImg;

    private bool selectedDice;

    private void Update()
    {
        if (selectedDice)
        {
            DisplayArrows();
        }
    }

    private void Start()
    {
        dice = GetComponent<DiceController>();

        diceBtn.interactable = false;

        targetArrow = UiManager.Instance.AddTargetArrow();

        if(targetArrow != null)
        {
            targetArrow.InitPosition(transform);
        }


        if (dice.character.stat.IsCharacterStaggered())
        {
            StaggeredDice();
        }

        else
        {
            DiceSpdRolling();
        }

        BattleManager.Instance.onRightClick += RightClick;

        dice.character.stat.onStaggered.AddListener(() =>
        {
            StaggeredDice();
        });

        dice.character.stat.unStaggered.AddListener(() =>
        {

            if (dice.character.IsEnemy) diceImg.sprite = ResourceManager.Instance.diceResource.enemyDice;

            else diceImg.sprite = ResourceManager.Instance.diceResource.teamDice;

            diceRoulette.gameObject.SetActive(true);
        });

    }

    private void RightClick()
    {
        if (dice.choosingCard != null)
        {
            dice.choosingCard.UnSelectCard();

            dice.character.stat.diceHandler.ResetLightAnim();
            dice.choosingCard = null;
        }
        UnSelectDice();
        HideArrows();
        UiManager.Instance.HideInfo();

    }

    public void DiceHoverInfo()
    {
        diceSelect.SetActive(true);
        DisplayArrows();

        if (dice.character.IsEnemy) UiManager.Instance.enemyInfoPanel.HoverInfo(dice);
        else UiManager.Instance.teamInfoPanel.HoverInfo(dice);


    }

    public void DiceExitInfo()
    {
        if (!selectedDice)
        {
            diceSelect.SetActive(false);
            HideArrows();

            if (dice.character.IsEnemy) UiManager.Instance.enemyInfoPanel.ExitInfo();
            else UiManager.Instance.teamInfoPanel.ExitInfo();
        }

    }


    public void DiceSpdRolling()
    {
        diceRoulette.gameObject.SetActive(true);
    }

    public void DiceSpdDecide()
    {
        diceBtn.interactable = true;
        diceRoulette.gameObject.SetActive(false);

        if (!dice.character.stat.IsCharacterStaggered())
        {
           
            rollSpd.gameObject.SetActive(false);

            StringBuilder sb = new StringBuilder();

            string valueString = dice.diceSpd.ToString();

            for (int i = 0; i < valueString.Length; i++)
            {
                if (dice.character.IsEnemy) sb.Append("<sprite=" + valueString[i] + ", color=#FF73FF>");
                else sb.Append("<sprite=" + valueString[i] + ", color=#FFF000>");

            }
            decidedSpd.text = sb.ToString();

            decidedSpd.gameObject.SetActive(true);

        }
    }

    public void StartBattle()
    {
        diceBtn.interactable = false;
        selectedDice = false;
        HideArrows();
        targetArrow.EndChoosingCard();
        decidedSpd.gameObject.SetActive(false);
        rollSpd.gameObject.SetActive(true);
        diceSelect.SetActive(false);
        diceGlow.SetActive(false);
    }

    public void ChoosingCard()
    {
        targetArrow.ChoosingCard();
    }

    public void StaggeredDice()
    {
        diceImg.sprite = ResourceManager.Instance.diceResource.staggeredDice;
        diceRoulette.gameObject.SetActive(false);
        rollSpd.gameObject.SetActive(false);
    }

    public void SelectDice()
    {
        selectedDice = true;

        diceSelect.SetActive(true);
    }

    public void UnSelectDice()
    {
        selectedDice = false;

        targetArrow.EndChoosingCard();
        HideArrows();
        diceSelect.SetActive(false);
    }

    public void OneWayDice(DiceUi target)
    {

        DiceEquipCard();

        targetArrow.SetArrowOneWay(target.transform, dice.character.IsEnemy);

    }

    private void DiceEquipCard()
    {
        diceGlow.SetActive(true);
        diceGlowAnim.SetBool("Equipped", true);
        diceSelect.SetActive(false);
    }

    public void UnEquipCard()
    {

        targetArrow.EndChoosingCard();
        targetArrow.HideArrow();

        diceGlowAnim.SetBool("Equipped", false);

        diceGlow.SetActive(false);
    }

    public void ClashingDice(DiceUi target)
    {
        DiceEquipCard();

        targetArrow.SetArrowClashing(target.transform);
        target.targetArrow.SetArrowClashing(transform);
    }




    public void DisplayArrows()
    {
        if(dice.target != null)
        {
            targetArrow.DisplayArrow();
        }


        if (dice.attacked.Count > 0)
        {

            for (int i = 0; i < dice.attacked.Count; i++)
            {
                dice.attacked[i].diceUi.targetArrow.DisplayArrow();
            }

        }

        if (dice.onesideAttacked.Count > 0)
        {

            for (int i = 0; i < dice.onesideAttacked.Count; i++)
            {
                dice.onesideAttacked[i].diceUi.targetArrow.DisplayArrow();
            }

        }


    }

    public void HideArrows()
    {
        if (dice.target != null)
        {
            targetArrow.HideArrow();
        }


        if (dice.attacked.Count > 0)
        {

            for (int i = 0; i < dice.attacked.Count; i++)
            {

                dice.attacked[i].diceUi.targetArrow.HideArrow();
            }

        }

        if (dice.onesideAttacked.Count > 0)
        {

            for (int i = 0; i < dice.onesideAttacked.Count; i++)
            {
                dice.onesideAttacked[i].diceUi.targetArrow.HideArrow();
            }

        }

    }

}
