using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "불빠따", menuName = "Custom/PassiveEffects/FlamingBat")]
public class PassiveEffect_FlamingBat : PassiveEffect
{
    public int burnAmount;

    public override void EquipEffect(Character character)
    {




        character.behaviour.onAttackHit += Effect;

    }

    private void Effect(Character atker, Character target)
    {

        // 공격 적중시 50%의 확률로 화상 1 부여
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            //BuffManager.Instance.GetBurn(target, burnAmount);
        }
    }

}
