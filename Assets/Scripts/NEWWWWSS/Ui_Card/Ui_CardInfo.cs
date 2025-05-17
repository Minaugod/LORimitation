using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;
using EnumTypes;
using UnityEngine.Pool;
public class Ui_CardInfo : MonoBehaviour
{
    public CardData cardData { get; private set; }

    public Image cardImage;

    public Image cardCostImage;

    public TMP_Text cardNameText;

    public Image[] glowImages;

    public SpriteAtlas cardCostAtlas;

    public Ui_CardDice[] ui_CardDices;

    [Header("CardColors")]
    public Color paperbackColor;

    public Color hardcoverColor;

    public Color limitedColor;


    public void SetCard(CardData cardData)
    {
        cardNameText.text = cardData.cardName;

        cardImage.sprite = cardData.image;

        cardCostImage.sprite = cardCostAtlas.GetSprite(cardData.cardCost.ToString());

        SetDice(cardData.dice);



        foreach (Image image in glowImages)
        {
            image.color = GetRarityColor(cardData.rarity);
        }


        this.cardData = cardData;

    }

    void SetDice(Dice[] dices)
    {
        for(int i = 0; i < ui_CardDices.Length; ++i)
        {
            if(dices.Length > i)
            {
                ui_CardDices[i].SetDice(dices[i]);
            }

            else
            {
                ui_CardDices[i].gameObject.SetActive(false);
            }
        }
    }

    Color GetRarityColor(Rarity rarity)
    {
        Color rarityColor = Color.white;
        
        switch (rarity)
        {
            case Rarity.Paperback:
                rarityColor = paperbackColor;
                break;

            case Rarity.Hardcover:
                rarityColor = hardcoverColor;
                break;

            case Rarity.Limited:
                rarityColor = limitedColor;
                break;
        }

        return rarityColor;
    }

}
