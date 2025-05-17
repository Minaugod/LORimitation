using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "사용_수호자", menuName = "Custom/CardUseEffect/수호자")]
public class UseEffect_Guardian : CardUseEffect
{

    public int enduranceAmount;

    public override void ApplyEffect(Character character)
    {

        // 모든 아군에게 다음 막에 인내 1 부여

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