using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Ui_EquippedCard : MonoBehaviour
{

    public CardData cardData { get; private set; }

    [SerializeField]
    private Ui_CardInfo ui_CardInfo;

    public event Action<CardData> onCardUnEquip;

    [SerializeField]
    private Ui_CardInfo unEquippedCard;

    [SerializeField]
    private Animator cardAnim;


    public void CardSelect()
    {
        unEquippedCard.SetCard(ui_CardInfo.cardData);
        unEquippedCard.transform.position = transform.position;
        cardAnim.Play("UnEquipped");

        onCardUnEquip?.Invoke(ui_CardInfo.cardData);
    }

    public void SetCard(CardData cardData)
    {
        ui_CardInfo.SetCard(cardData);
        ui_CardInfo.gameObject.SetActive(true);
    }


    public void UnSetCard()
    {
        ui_CardInfo.gameObject.SetActive(false);
    }


}
