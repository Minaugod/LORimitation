using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Ui_KeyPageInfo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text keyPageName;

    [SerializeField]
    private TMP_Text keyPageNameUnderlay;

    [SerializeField]
    private TMP_Text hpText;

    [SerializeField]
    private TMP_Text staggerText;

    [SerializeField]
    private TMP_Text spdText;

    [SerializeField]
    private Ui_KeyPageResist[] keyPageResists;

    [SerializeField]
    private Image thumbImage;

    [SerializeField]
    private Ui_EquippedCard[] equippedCards;

    [SerializeField]
    private Animator anim;

    private void OnEnable()
    {
        anim.SetBool("Showing", true);
    }

    private void OnDisable()
    {
        anim.SetBool("Showing", false);
    }

    public void InitInfo(KeyPage keyPage)
    {

        keyPageName.text = string.Format("{0}의 책장", keyPage.page.pageName);
        keyPageNameUnderlay.text = string.Format("{0}의 책장", keyPage.page.pageName);

        hpText.text = keyPage.page.hp.ToString();
        staggerText.text = keyPage.page.staggerResist.ToString();

        spdText.text = string.Format("{0}~{1}", keyPage.page.spdDiceMin, keyPage.page.spdDiceMax);

        thumbImage.sprite = keyPage.page.thumbSprite;


        SetCard(keyPage);

    }


    void SetCard(KeyPage keyPage)
    {
        for(int i = 0; i < equippedCards.Length; ++i)
        {
            if(keyPage.cards.Count > i)
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
