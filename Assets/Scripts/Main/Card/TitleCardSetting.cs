using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TitleCardSetting : MonoBehaviour
{

    private TitleEquippedCard[] equippedCards;

    private TitleEquipableCard[] equipableCards;

    [SerializeField]
    private List<CardData> holdCards = new List<CardData>();

    private CharacterBaseInfo character;

    public event Action OnSaveSetting;

    [SerializeField]
    private List<CardData> cardList = new List<CardData>();

    public void DisplayCardInfo(CharacterBaseInfo character)
    {
        
        
        gameObject.SetActive(true);
        holdCards.Clear();
        this.character = character;



        for(int i = 0; i < character.keyPage.cards.Count; i++)
        {
            holdCards.Add(character.keyPage.cards[i]);
        }

        RefreshCards();

    }

    private void RefreshCards()
    {

        holdCards.Sort((a, b) => a.cardCost.CompareTo(b.cardCost));

        for (int i = 0; i < equippedCards.Length; i++)
        {
            if(holdCards.Count > i)
            {
                equippedCards[i].CardInit(holdCards[i]);
            }
            else
            {
                equippedCards[i].UnEquipCard();
            }


        }
    }


    public void EquipCard(CardData card)
    {

        if(holdCards.Count < 9)
        {
            holdCards.Add(card);

            RefreshCards();
        }

    }

    public void UnEquipCard(CardData card)
    {
        holdCards.Remove(card);

        RefreshCards();

    }

    private void Awake()
    {
        equipableCards = GetComponentsInChildren<TitleEquipableCard>();
        equippedCards = GetComponentsInChildren<TitleEquippedCard>();

    }

    private void Start()
    {
        foreach (TitleEquippedCard equipped in equippedCards)
        {
            equipped.OnCardClicked += UnEquipCard;
        }

        cardList.Sort((a, b) => a.cardCost.CompareTo(b.cardCost));

        for (int i = 0; i < equipableCards.Length; i++)
        {
            equipableCards[i].OnCardClicked += EquipCard;
            equipableCards[i].CardInit(cardList[i]);
        }


    }

    public void SaveCard()
    {

        if(holdCards.Count == 9)
        {
            character.keyPage.cards.Clear();

            foreach (CardData card in holdCards)
            {
                character.keyPage.cards.Add(card);
            }

            gameObject.SetActive(false);
            OnSaveSetting?.Invoke();
        }


    }

}
