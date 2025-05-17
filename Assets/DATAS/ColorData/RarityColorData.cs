using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

[CreateAssetMenu(fileName = "RarityColorData", menuName = "ScriptableObjects/RarityColorData")]
public class RarityColorData : ScriptableObject
{

    public Color paperbackColor;
    public Color hardcoverColor;
    public Color limitedColor;

    public Color GetColor(Rarity rarity)
    {
        return rarity switch
        {
            Rarity.Paperback => paperbackColor,
            Rarity.Hardcover => hardcoverColor,
            Rarity.Limited => limitedColor,
            _ => paperbackColor
        };
    }


}
