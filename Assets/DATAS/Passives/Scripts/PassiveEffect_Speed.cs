using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "속도", menuName = "Custom/PassiveEffects/Speed")]
public class PassiveEffect_Speed : PassiveEffect
{
    public int diceAmount;

    // 속도주사위 + 1
    public override void EquipEffect(Character character)
    {


        character.stat.diceHandler.AddDice();

    }
}
