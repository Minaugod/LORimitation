using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "���_����ѹ�", menuName = "Custom/CardUseEffect/����ѹ�")]
public class UseEffect_SilentNight : CardUseEffect
{

    public int enduranceAmount;

    public override void ApplyEffect(Character character)
    {

        // ���� ���� �γ� 2�� ����

        //BuffManager.Instance.GetEndurance(character, enduranceAmount);


    }


}