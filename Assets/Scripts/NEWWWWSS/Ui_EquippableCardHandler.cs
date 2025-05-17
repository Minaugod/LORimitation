using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Ui_EquippableCardHandler : MonoBehaviour
{

    KeyPage currentKeyPage;
    
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Transform cardsTransform;

    [SerializeField]
    private Ui_CardDetail ui_CardDetail;

    [SerializeField]
    private Vector3 cardDetailOffset;

    public CardData[] cardDatas;

    List<Ui_EquippableCard> equippableCards = new List<Ui_EquippableCard>();

    [SerializeField]
    private Scrollbar scrollbar;

    public event Action onCardEquipped;

    private void Awake()
    {
        cardDatas = Resources.LoadAll<CardData>("CardSO");

    }

    private void Start()
    {
        foreach(CardData cardData in cardDatas)
        {
            Ui_EquippableCard ui_Card = Instantiate(cardPrefab).GetComponent<Ui_EquippableCard>();

            ui_Card.onCardSelect += EquipCard;
            ui_Card.onCardHover += ShowCardDetail;
            ui_Card.onExitHover += HideCardDetail;

            ui_Card.transform.SetParent(cardsTransform, false);

            ui_Card.SetCard(cardData);

            equippableCards.Add(ui_Card);
        }
    }

    void EquipCard(CardData cardData)
    {
        if(currentKeyPage.cards.Count < 9)
        {
            // Equip
            currentKeyPage.cards.Add(cardData);

            onCardEquipped?.Invoke();
        }
    }

    private void OnEnable()
    {
        scrollbar.value = 1;
    }

    public void InitInfo(Librarian librarian)
    {
        currentKeyPage = librarian.keyPage;
    }

    void ShowCardDetail(Ui_EquippableCard equippableCard)
    {
        ui_CardDetail.gameObject.SetActive(true);
        ui_CardDetail.transform.position = equippableCard.transform.position + cardDetailOffset;
        ui_CardDetail.SetCard(equippableCard.cardData); 

    }
    void HideCardDetail()
    {
        ui_CardDetail.gameObject.SetActive(false);
    }


}
