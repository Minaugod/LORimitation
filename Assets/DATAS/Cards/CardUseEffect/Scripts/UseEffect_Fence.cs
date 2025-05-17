using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "사용_울타리", menuName = "Custom/CardUseEffect/울타리")]
public class UseEffect_Fence : CardUseEffect
{
    public int protectionAmount;
    public override void ApplyEffect(Character character)
    {

        // 사용시 모든 아군아게 다음 막에 보호 1 부여
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
