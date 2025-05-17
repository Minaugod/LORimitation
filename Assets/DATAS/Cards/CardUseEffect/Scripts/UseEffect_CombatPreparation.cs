using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "���_�����غ�", menuName = "Custom/CardUseEffect/�����غ�")]
public class UseEffect_CombatPreparation : CardUseEffect
{

    public int enduranceAmount;

    public override void ApplyEffect(Character character)
    {

        // ���� ��� �Ʊ����� ���� ���� �γ� 2 �ο�
        if (character.IsEnemy)
        {
            for(int i = 0; i < BattleManager.Instance.enemyList.Count; i++)
            {
                BuffManager.Instance.GetEndurance(BattleManager.Instance.enemyList[i], enduranceAmount);
                
            }
        }

        else
        {
            for (int i = 0; i < BattleManager.Instance.teamList.Count; i++)
            {
                BuffManager.Instance.GetEndurance(BattleManager.Instance.teamList[i], enduranceAmount);

            }
        }

    }
}
