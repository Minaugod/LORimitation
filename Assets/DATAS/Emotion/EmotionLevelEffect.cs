using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmotionLevelEffect", menuName = "Custom/EmotionLevelEffect/effect")]
public class EmotionLevelEffect : ScriptableObject
{

    public string effectLv;

    public int requireEmotionCoin;

    public bool addMaxLight;

    public bool addSpeedDice;

    public void ApplyEffect(Character character)
    {

        if (addMaxLight) character.stat.diceHandler.AddLight();



        if (addSpeedDice) character.stat.diceHandler.AddDice();

        character.stat.RechargeAllLight();

    }
}
