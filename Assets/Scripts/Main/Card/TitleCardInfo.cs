using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleCardInfo : MonoBehaviour
{


    [SerializeField]
    private Image cardDefaultFrame;


    [SerializeField]
    private Image cardCost;

    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text cardName;

    [SerializeField]
    private Image[] defaultDice;




    public void EquipCard(CardData card)
    {


        Color cardRarityColor = ResourceManager.Instance.cardResource.GetCardRarityColor(card.rarity);

        cardDefaultFrame.color = cardRarityColor;
        cardCost.color = cardRarityColor;

        cardCost.sprite = ResourceManager.Instance.cardResource.cardCost[card.cardCost];
        cardName.text = card.cardName;
        image.sprite = card.image;

        for (int i = 0; i < 3; i++)
        {
            if (i < card.dice.Length)
            {
                defaultDice[i].sprite = ResourceManager.Instance.cardResource.GetDiceSprite(card.dice[i].diceType);          

                defaultDice[i].gameObject.SetActive(true);                            
            }

            else
            {
                defaultDice[i].gameObject.SetActive(false);                
            }

        }

    }
}
