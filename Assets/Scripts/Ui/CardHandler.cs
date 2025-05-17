using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{

    [SerializeField]
    private CardController[] cards;

    private void Awake()
    {
        cards = GetComponentsInChildren<CardController>();
    }

    public void CardInit(Character character)
    {
        gameObject.SetActive(true);


        for (int i = 0; i < 9; i++)
        {
            cards[i].id = i;
            cards[i].CardInit(character.stat.keyPage.cards[i]);

            if (!character.stat.haveCards[i])
            {
                cards[i].gameObject.SetActive(false);
            }

            else
            {
                cards[i].gameObject.SetActive(true);
            }
        }
    }

    public void HideCard()
    {
        gameObject.SetActive(false);
    }


}
