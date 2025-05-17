using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardController : MonoBehaviour, IPointerDownHandler
{

    public int id;

    public CardData cardData;

    [SerializeField]
    private Image cardDefaultFrame;

    [SerializeField]
    private Image cardInfoFrame;

    [SerializeField]
    private Image cardCost;

    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text cardName;

    [SerializeField]
    private TMP_Text useEffect;


    //public CardDiceUiElements[] dices;

    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UnSelectCard()
    {
        animator.SetBool("Select", false);
        animator.SetBool("Show", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SelectCard();
        }
    }
    public void SelectCard()
    {
        DiceController dice = BattleManager.Instance.selectDice;
        
        if (dice.choosingCard == null && dice.selectCard == null && dice.character.stat.haveLight >= cardData.cardCost)
        {
            dice.choosingCard = this;
            animator.SetBool("Select", true);

            int light = dice.character.stat.haveLight;
            for (int i = light - 1; i >= light - cardData.cardCost; i--)
            {
                dice.character.stat.diceHandler.lights[i].BlinkLight();
            }

            dice.diceUi.ChoosingCard();
        }
       
    }
    public void CardInit(CardData card)
    {



        cardData = card;


        Color cardRarityColor = ResourceManager.Instance.cardResource.GetCardRarityColor(cardData.rarity);

        cardDefaultFrame.color = cardRarityColor;
        cardInfoFrame.color = cardRarityColor;
        cardCost.color = cardRarityColor;


        cardCost.sprite = ResourceManager.Instance.cardResource.cardCost[cardData.cardCost];

        cardName.text = cardData.cardName;
        image.sprite = cardData.image;

        useEffect.gameObject.SetActive(false);
        // 사용시 효과 표시    
        if (cardData.useEffect != null)
        {
            useEffect.gameObject.SetActive(true);
            useEffect.text = cardData.useEffect.desc;
        }

        /*
        for (int i = 0; i < dices.Length; i++)
        {
            if (i < cardData.dice.Length)
            {
                dices[i].defaultDiceType.sprite = ResourceManager.Instance.cardResource.GetDiceSprite(cardData.dice[i].diceType);
                dices[i].infoDiceType.sprite = ResourceManager.Instance.cardResource.GetDiceSprite(cardData.dice[i].diceType);

                dices[i].diceValue.text = string.Format("{0}-{1}", cardData.dice[i].diceMin, cardData.dice[i].diceMax);

                if (cardData.dice[i].diceType is EnumTypes.DiceType.Slash or EnumTypes.DiceType.Pierce or EnumTypes.DiceType.Blunt)
                {
                    dices[i].diceValue.color = ResourceManager.Instance.cardResource.attackTypeColor;
                }

                else
                {
                    dices[i].diceValue.color = ResourceManager.Instance.cardResource.defenseTypeColor;
                }

                dices[i].diceEffect.text = cardData.dice[i].diceEffect.desc;

                dices[i].defaultDiceType.gameObject.SetActive(true);
                dices[i].infoDiceObj.SetActive(true);
            }

            else
            {
                dices[i].defaultDiceType.gameObject.SetActive(false);
                dices[i].infoDiceObj.SetActive(false);
            }
        }
        */

    }

    public void CardHover()
    {

        if(BattleManager.Instance.selectDice.choosingCard == null &&
            BattleManager.Instance.selectDice.selectCard == null)
        {
            animator.SetBool("Show", true);
        }

    }

    public void CardExit()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {

            if (BattleManager.Instance.selectDice.choosingCard == null &&
                BattleManager.Instance.selectDice.selectCard == null)
            {
                animator.Play("IDLE");

            }
        }   

            
        animator.SetBool("Show", false);

    }

}
