using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class Ui_EquippableCard : MonoBehaviour
{

    public CardData cardData { get; private set; }

    [SerializeField]
    private Ui_CardInfo ui_CardInfo;


    public event Action<CardData> onCardSelect;

    public event Action<Ui_EquippableCard> onCardHover;

    public event Action onExitHover;

    public void SetCard(CardData cardData)
    {       
        ui_CardInfo.SetCard(cardData);

        this.cardData = cardData;
    }

    public void InitKeyPage()
    {

    }

    public void CardSelect()
    {

        onCardSelect?.Invoke(cardData);
    }

    public void HoverCard()
    {
        onCardHover?.Invoke(this);
    }

    public void ExitHover()
    {
        onExitHover?.Invoke();
    }
}
