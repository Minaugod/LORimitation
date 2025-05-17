using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "그냥피해", menuName = "Custom/DiceUseEffects/Damage")]
public class DiceUseEffect_Damage : DiceUseEffect
{
    public override void ApplyEffect(Character user, Character target, int value)
    {

        target.stat.TakeJustDamage(value);



    }
}
