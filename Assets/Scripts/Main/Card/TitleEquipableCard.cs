using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class TitleEquipableCard : MonoBehaviour
{

    private CardData cardData;

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



    [SerializeField]
    private Animator animator;

    public event Action<CardData> OnCardClicked;

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
        for(int i = 0; i < dices.Length; i++)
        {
            if(i < cardData.dice.Length)
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


    public void HoverUi()
    {

        animator.SetBool("Flip", true);

    }

    public void ExitUi()
    {
        animator.SetBool("Flip", false);
    }

    public void ClickCard()
    {
        if (cardData != null)
        {
            OnCardClicked?.Invoke(cardData);
            ExitUi();
        }

    }



}
