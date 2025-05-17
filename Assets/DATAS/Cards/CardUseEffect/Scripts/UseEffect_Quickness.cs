using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "사용_재빠름", menuName = "Custom/CardUseEffect/재빠름")]
public class UseEffect_Quickness : CardUseEffect
{

    public int hasteAmount;
    public override void ApplyEffect(Character character)
    {

        BuffManager.Instance.GetHaste(character, hasteAmount);

    }


}