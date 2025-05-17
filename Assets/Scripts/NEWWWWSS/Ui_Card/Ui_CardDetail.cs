using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;
using System;
using UnityEngine.Pool;
public class Ui_CardDetail : MonoBehaviour
{
    CardData cardData;

    /*
    [SerializeField]
    private GameObject dicePrefab;

    [SerializeField]
    private GameObject detailDicePrefab;

    [Header("Card Detail Elements")]
    private IObjectPool<Ui_CardDice> _DicePool;

    private IObjectPool<Ui_CardDetailDice> _DetailDicePool;

    public Transform leftDicesTf;

    public Transform rightDicesTf;

    */

    public Ui_CardDice[] ui_CardDices;

    public Ui_CardDetailDice[] ui_CardDetailDices;

    public Image cardDetailImage;

    public Image cardDetailCostImage;

    public TMP_Text cardDetailNameText;

    public TMP_Text cardUseEffectText;

    public Image[] glowImages;

    public SpriteAtlas cardCostAtlas;

    [SerializeField]
    private Animator anim;

    [Header("CardColors")]
    public Color paperbackColor;

    public Color hardcoverColor;

    public Color limitedColor;


    private void OnEnable()
    {
        anim.SetBool("Showing", true);
    }

    private void OnDisable()
    {
        anim.SetBool("Showing", false);
    }

    public void SetCard(CardData cardData)
    {

        cardDetailNameText.text = cardData.cardName;

        cardDetailImage.sprite = cardData.image;

        cardDetailCostImage.sprite = cardCostAtlas.GetSprite(cardData.cardCost.ToString());

        if (cardData.useEffect != null)
        {
            cardUseEffectText.gameObject.SetActive(true);
            cardUseEffectText.text = cardData.useEffect.desc;
        }

        else cardUseEffectText.gameObject.SetActive(false);

        SetDice(cardData.dice);
        
        SetColor(cardData.rarity);


        this.cardData = cardData;

    }

    void SetDice(Dice[] dices)
    {
        for (int i = 0; i < ui_CardDices.Length; ++i)
        {
            if (dices.Length > i)
            {
                ui_CardDices[i].SetDice(dices[i]);
                ui_CardDetailDices[i].SetDice(dices[i]);
            }

            else
            {
                ui_CardDices[i].gameObject.SetActive(false);
                ui_CardDetailDices[i].gameObject.SetActive(false);
            }
        }
    }

    void SetColor(EnumTypes.Rarity rarity)
    {
        Color rarityColor = Color.white;

        switch (rarity)
        {
            case EnumTypes.Rarity.Paperback:
                rarityColor = paperbackColor;
                break;

            case EnumTypes.Rarity.Hardcover:
                rarityColor = hardcoverColor;
                break;

            case EnumTypes.Rarity.Limited:
                rarityColor = limitedColor;
                break;
        }

        foreach (Image image in glowImages)
        {
            image.color = rarityColor;
        }
    }



}
