using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.Pool;
public class Ui_CardDice : MonoBehaviour
{


    public Image diceImage;

    public SpriteAtlas diceTypeAtlas;



    public void SetDice(Dice diceData)
    {
        diceImage.sprite = diceTypeAtlas.GetSprite(diceData.diceType.ToString());
        gameObject.SetActive(true);
    }


}

