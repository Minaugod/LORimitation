using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "���_��ȣ��", menuName = "Custom/CardUseEffect/��ȣ��")]
public class UseEffect_Guardian : CardUseEffect
{

    public int enduranceAmount;

    public override void ApplyEffect(Character character)
    {

        // ��� �Ʊ����� ���� ���� �γ� 1 �ο�

        if (character.IsEnemy)
        {
            for (int i = 0; i < BattleManager.Instance.enemyList.Count; i++)
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