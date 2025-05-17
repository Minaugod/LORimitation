using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "2´Ü Â÷±â", menuName = "Custom/PassiveEffects/DoubleKick")]
public class PassiveEffect_DoubleKick : PassiveEffect
{
    public int staggerDmgAmount;

    public override void EquipEffect(Character character)
    {


        character.behaviour.onWinClash += DoubleKick;


    }

    private void DoubleKick(Character winner, Character loser)
    {

        //target.GetDamage(staggerDmgAmount);
    }

}

