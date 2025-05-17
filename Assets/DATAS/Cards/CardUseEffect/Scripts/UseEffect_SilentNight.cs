using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "사용_고요한밤", menuName = "Custom/CardUseEffect/고요한밤")]
public class UseEffect_SilentNight : CardUseEffect
{

    public int enduranceAmount;

    public override void ApplyEffect(Character character)
    {

        // 다음 막에 인내 2를 얻음

        //BuffManager.Instance.GetEndurance(character, enduranceAmount);


    }


}