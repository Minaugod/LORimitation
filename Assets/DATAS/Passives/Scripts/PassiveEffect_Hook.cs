using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hook", menuName = "Custom/PassiveEffects/Hook")]
public class PassiveEffect_Hook : PassiveEffect
{
    public int strengthAmount;

    public override void EquipEffect(Character character)
    {


        character.behaviour.onAttackHit += Effect;

    }


    private void Effect(Character atker, Character target)
    {
                /*
        if (target.Hp <= 0)
        {
            BuffManager.Instance.GetStrength(character, strengthAmount);
        }
        */
    }
}