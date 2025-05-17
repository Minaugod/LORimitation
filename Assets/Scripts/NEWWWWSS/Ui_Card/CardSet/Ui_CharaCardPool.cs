using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ui_CharaCardPool : MonoBehaviour
{
    [SerializeField]
    private Ui_EquippedCard[] equippedCards;

    [SerializeField]
    private Image colorChangingImage;

    [SerializeField]
    private Color selectColor;

    Color originalColor;

    bool isSelected = false;

    private void Start()
    {
        originalColor = colorChangingImage.color;
    }

    public void HoverUi()
    {

        if (!isSelected)
        {
            colorChangingImage.color = selectColor;
        }

    }

    public void ExitUi()
    {
        if (!isSelected)
        {
            colorChangingImage.color = originalColor;
        }
    }
    public void SelectSetting()
    {
        isSelected = true;
        colorChangingImage.color = selectColor;
    }

    public void DeSelectSetting()
    {
        isSelected = false;
        colorChangingImage.color = originalColor;
    }

    public void RefreshCardPool(KeyPage keyPage)
    {


        for (int i = 0; i < equippedCards.Length; ++i)
        {
            if (keyPage.cards.Count > i)
            {
                equippedCards[i].SetCard(keyPage.cards[i]);
            }

            else
            {
                equippedCards[i].UnSetCard();
            }
        }

    }
}
