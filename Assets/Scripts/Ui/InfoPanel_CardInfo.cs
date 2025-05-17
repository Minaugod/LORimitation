using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel_CardInfo : MonoBehaviour
{

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

    public void CardInit(CardData cardData)
    {
        gameObject.SetActive(true);

        Color cardRarityColor = ResourceManager.Instance.cardResource.GetCardRarityColor(cardData.rarity);

        cardDefaultFrame.color = cardRarityColor;
        cardInfoFrame.color = cardRarityColor;
        cardCost.color = cardRarityColor;


        cardCost.sprite = ResourceManager.Instance.cardResource.cardCost[cardData.cardCost];

        cardName.text = cardData.cardName;
        image.sprite = cardData.image;

        useEffect.gameObject.SetActive(false);

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

                if(cardData.dice[i].diceType is EnumTypes.DiceType.Slash or EnumTypes.DiceType.Pierce or EnumTypes.DiceType.Blunt)
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



}
