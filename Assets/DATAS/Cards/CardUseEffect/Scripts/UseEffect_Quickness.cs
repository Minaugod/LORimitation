using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "���_�����", menuName = "Custom/CardUseEffect/�����")]
public class UseEffect_Quickness : CardUseEffect
{

    public int hasteAmount;
    public override void ApplyEffect(Character character)
    {

        BuffManager.Instance.GetHaste(character, hasteAmount);

    }


}