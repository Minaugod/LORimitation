using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "���_��Ÿ��", menuName = "Custom/CardUseEffect/��Ÿ��")]
public class UseEffect_Fence : CardUseEffect
{
    public int protectionAmount;
    public override void ApplyEffect(Character character)
    {

        // ���� ��� �Ʊ��ư� ���� ���� ��ȣ 1 �ο�
        if (character.IsEnemy)
        {
            for (int i = 0; i < BattleManager.Instance.enemyList.Count; i++)
            {
                BuffManager.Instance.GetProtection(BattleManager.Instance.enemyList[i], protectionAmount);

            }
        }

        else
        {
            for (int i = 0; i < BattleManager.Instance.teamList.Count; i++)
            {
                BuffManager.Instance.GetProtection(BattleManager.Instance.teamList[i], protectionAmount);

            }
        }

    }
}
