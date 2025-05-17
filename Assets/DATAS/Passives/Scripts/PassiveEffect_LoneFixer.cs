using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "���� �ذ��", menuName = "Custom/PassiveEffects/LoneFixer")]
public class PassiveEffect_LoneFixer : PassiveEffect
{
    public int strengthAmount;

    public override void EquipEffect(Character character)
    {

        character.behaviour.onNextAct += ActiveEffect;

    }

    private void ActiveEffect(Character character)
    {

        // �� ����� �ٸ� �Ʊ��� ������ ���� ���� ���� ����

        int teamCount;

        if (character.IsEnemy) teamCount = BattleManager.Instance.enemyList.Count;

        else teamCount = BattleManager.Instance.teamList.Count;

        if (teamCount == 1)
        {

            BuffManager.Instance.GetStrength(character, strengthAmount);

        }
    }


}

