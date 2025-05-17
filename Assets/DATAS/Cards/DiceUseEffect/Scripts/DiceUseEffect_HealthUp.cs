using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "체력회복", menuName = "Custom/DiceUseEffects/HealthUp")]
public class DiceUseEffect_HealthUp : DiceUseEffect
{
    public override void ApplyEffect(Character user, Character target, int value)
    {

        user.stat.Hp += value;
        UiManager.Instance.DisplayHeal(user.transform, value);

    }
}
