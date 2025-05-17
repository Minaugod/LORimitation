using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "��ó ����", menuName = "Custom/PassiveEffects/OpenWound")]
public class PassiveEffect_OpenWound : PassiveEffect
{

    public int bleedAmount;

    // ���� ���߽� 50%�� Ȯ���� ���� 1 �ο�
    public override void EquipEffect(Character character)
    {


        character.behaviour.onAttackHit += Effect;


    }

    private void Effect(Character atker, Character target)
    {

        // ���� ���߽� 50%�� Ȯ���� ���� 1 �ο�
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            //BuffManager.Instance.GetBleed(target, bleedAmount);
        }
    }

}