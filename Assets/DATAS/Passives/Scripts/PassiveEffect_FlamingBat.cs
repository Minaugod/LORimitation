using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "�Һ���", menuName = "Custom/PassiveEffects/FlamingBat")]
public class PassiveEffect_FlamingBat : PassiveEffect
{
    public int burnAmount;

    public override void EquipEffect(Character character)
    {




        character.behaviour.onAttackHit += Effect;

    }

    private void Effect(Character atker, Character target)
    {

        // ���� ���߽� 50%�� Ȯ���� ȭ�� 1 �ο�
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            //BuffManager.Instance.GetBurn(target, burnAmount);
        }
    }

}
