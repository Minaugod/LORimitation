using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Ui_EquippedCardHandler : MonoBehaviour
{
    KeyPage currentKeyPage;

    [SerializeField]
    private TMP_Text librarianNameText;

    [SerializeField]
    private Ui_EquippedCard[] equippedCards;

    public event Action onCardUnEquipped;

    private void Start()
    {
        foreach (Ui_EquippedCard equippedCard in equippedCards) 
        {
            equippedCard.onCardUnEquip += UnEquipCard;
        }
    }

    void UnEquipCard(CardData cardData)
    {
        currentKeyPage.cards.Remove(cardData);

        onCardUnEquipped?.Invoke();

    }


    public void InitInfo(Librarian librarian)
    {
        currentKeyPage = librarian.keyPage;

        librarianNameText.text = librarian.name;

        RefreshCardPool();
    }

    public void RefreshCardPool()
    {

        for (int i = 0; i < equippedCards.Length; ++i)
        {
            if (currentKeyPage.cards.Count > i)
            {
                equippedCards[i].SetCard(currentKeyPage.cards[i]);
            }

            else
            {
                equippedCards[i].UnSetCard();
            }
        }
    }

}
