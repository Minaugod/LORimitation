using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "상처 찢기", menuName = "Custom/PassiveEffects/OpenWound")]
public class PassiveEffect_OpenWound : PassiveEffect
{

    public int bleedAmount;

    // 공격 적중시 50%의 확률로 출혈 1 부여
    public override void EquipEffect(Character character)
    {


        character.behaviour.onAttackHit += Effect;


    }

    private void Effect(Character atker, Character target)
    {

        // 공격 적중시 50%의 확률로 출혈 1 부여
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            //BuffManager.Instance.GetBleed(target, bleedAmount);
        }
    }

}