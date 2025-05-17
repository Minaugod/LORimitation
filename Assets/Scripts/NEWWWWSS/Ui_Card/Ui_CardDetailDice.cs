using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.Pool;
using TMPro;
public class Ui_CardDetailDice : MonoBehaviour
{
    public Image diceImage;

    public TMP_Text diceValueText;

    public TMP_Text diceUseEffectText;

    public SpriteAtlas diceTypeAtlas;

    public Color atkTypeColor;

    public Color defTypeColor;


    public void SetDice(Dice diceData)
    {
        gameObject.SetActive(true);

        diceImage.sprite = diceTypeAtlas.GetSprite(diceData.diceType.ToString());

        if (diceData.diceType is EnumTypes.DiceType.Block or EnumTypes.DiceType.Evade)
        {
            diceValueText.color = defTypeColor;
        }

        else
        {
            diceValueText.color = atkTypeColor;
        }


        diceValueText.text = string.Format("{0}-{1}", diceData.diceMin, diceData.diceMax);

        diceUseEffectText.text = diceData.diceEffect.desc;

    }

}
