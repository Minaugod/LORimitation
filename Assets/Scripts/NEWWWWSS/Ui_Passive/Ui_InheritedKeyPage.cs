using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class Ui_InheritedKeyPage : MonoBehaviour
{

    [SerializeField] private Image frameImage;

    [SerializeField] private Image iconImage;

    [SerializeField] private Image glowIconImage;

    [SerializeField] private TMP_Text keyPageName;

    [SerializeField] private List<Ui_InheritablePassive> ui_inheritablePassives = new List<Ui_InheritablePassive>();

    public List<Ui_InheritablePassive> Ui_Passives { get { return ui_inheritablePassives; } }


    [SerializeField] private RarityColorData colorData;



    public void InitKeyPage(InheritingKeyPage inheritingKeyPage)
    {

        KeyPageData pageData = inheritingKeyPage.keyPage.PageData;

        iconImage.sprite = pageData.iconSprite;
        glowIconImage.sprite = pageData.glowIconSprite;
        keyPageName.text = string.Format("{0}¿« √•¿Â", pageData.pageName);

        Color rarityColor = colorData.GetColor(pageData.rarity);

        frameImage.color = rarityColor;
        glowIconImage.color = rarityColor;
        keyPageName.color = rarityColor;


        List<InheritingKeyPage.InheritablePassive> inheritablePassives = inheritingKeyPage.inheritablePassives;

        for (int i = 0; i < ui_inheritablePassives.Count; ++i)
        {
            if(inheritablePassives.Count > i)
            {
                ui_inheritablePassives[i].InitPassive(inheritablePassives[i]);
            }

            else
            {
                ui_inheritablePassives[i].DisablePassive();
            }
        }

        gameObject.SetActive(true);

    }

    public void DisableKeyPage()
    {
        gameObject.SetActive(false);
    }





}
