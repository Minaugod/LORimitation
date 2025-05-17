using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "비상 식량", menuName = "Custom/PassiveEffects/EmergencyRations")]
public class PassiveEffect_EmergencyRations : PassiveEffect
{
    public int healAmount;

    public override void EquipEffect(Character character)
    {

        character.behaviour.onNextAct += EmergencyRations;



    }
    private void EmergencyRations(Character character)
    {

        // 막 시작시 체력 2 회복
        character.stat.Hp += healAmount;
        UiManager.Instance.DisplayHeal(character.transform, healAmount);

    }

}

