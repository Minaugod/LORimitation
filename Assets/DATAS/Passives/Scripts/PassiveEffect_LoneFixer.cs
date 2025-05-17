using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "고독한 해결사", menuName = "Custom/PassiveEffects/LoneFixer")]
public class PassiveEffect_LoneFixer : PassiveEffect
{
    public int strengthAmount;

    public override void EquipEffect(Character character)
    {

        character.behaviour.onNextAct += ActiveEffect;

    }

    private void ActiveEffect(Character character)
    {

        // 막 종료시 다른 아군이 없으면 다음 막에 힘을 얻음

        int teamCount;

        if (character.IsEnemy) teamCount = BattleManager.Instance.enemyList.Count;

        else teamCount = BattleManager.Instance.teamList.Count;

        if (teamCount == 1)
        {

            BuffManager.Instance.GetStrength(character, strengthAmount);

        }
    }


}

