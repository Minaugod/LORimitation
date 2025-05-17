using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "��� �ķ�", menuName = "Custom/PassiveEffects/EmergencyRations")]
public class PassiveEffect_EmergencyRations : PassiveEffect
{
    public int healAmount;

    public override void EquipEffect(Character character)
    {

        character.behaviour.onNextAct += EmergencyRations;



    }
    private void EmergencyRations(Character character)
    {

        // �� ���۽� ü�� 2 ȸ��
        character.stat.Hp += healAmount;
        UiManager.Instance.DisplayHeal(character.transform, healAmount);

    }

}

