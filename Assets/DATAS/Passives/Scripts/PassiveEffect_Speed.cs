using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "�ӵ�", menuName = "Custom/PassiveEffects/Speed")]
public class PassiveEffect_Speed : PassiveEffect
{
    public int diceAmount;

    // �ӵ��ֻ��� + 1
    public override void EquipEffect(Character character)
    {


        character.stat.diceHandler.AddDice();

    }
}
