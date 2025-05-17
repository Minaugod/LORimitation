using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ui_PassiveShapeSetter : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private TMP_Text descText;

    [SerializeField] private Image frameImage;

    [SerializeField] private RarityColorData colorData;

    [SerializeField] private Color normalColor;

    [SerializeField] private Color unInheritableColor;

    public void InitPassive(PassiveEffect passive)
    {


        costText.text = passive.passiveCost.ToString();

        nameText.text = passive.passiveName;

        descText.text = passive.passiveDesc;


        costText.color = colorData.GetColor(passive.rarity);

        nameText.color = normalColor;

        descText.color = normalColor;

        frameImage.color = colorData.GetColor(passive.rarity);


        gameObject.SetActive(true);

    }


    public void InheritedPassive()
    {

    }



}
