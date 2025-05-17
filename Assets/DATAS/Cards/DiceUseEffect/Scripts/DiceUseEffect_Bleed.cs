using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ÃâÇ÷ºÎ¿©", menuName = "Custom/DiceUseEffects/Bleed")]
public class DiceUseEffect_Bleed : DiceUseEffect
{
    public override void ApplyEffect(Character user, Character target, int value)
    {

        BuffManager.Instance.GetBleed(target, value);

    }
}
