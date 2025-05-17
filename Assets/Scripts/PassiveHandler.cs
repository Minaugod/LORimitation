using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PassiveHandler : MonoBehaviour
{


    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();

    }


    public void EquipPassiveEffects(List<PassiveEffect> passiveEffects)
    {
        foreach (PassiveEffect passiveEffect in passiveEffects)
        {
            passiveEffect.EquipEffect(character);
        }
    }

}
