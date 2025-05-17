using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseEffect_Track", menuName = "Custom/CardUseEffect/ÃßÀû")]
public class UseEffect_Track : CardUseEffect
{
    public override void ApplyEffect(Character character)
    {

        character.stat.Hp += 1;
        UiManager.Instance.DisplayHeal(character.transform, 1);

    }
}
