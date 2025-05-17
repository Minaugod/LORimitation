using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "�Ｎ ����", menuName = "Custom/PassiveEffects/InstantCooking")]
public class PassiveEffect_InstantCooking : PassiveEffect
{
    public override void EquipEffect(Character character)
    {

        // �� óġ�� ü�� 10% ȸ��(�ִ� 12)

        character.behaviour.onAttackHit += Effect;
    }


    private void Effect(Character atker, Character target)
    {
        if (target.stat.Hp <= 0)
        {
            int healthUp = atker.stat.keyPage.page.hp / 10;

            if (healthUp >= 12)
            {
                healthUp = 12;
            }

            atker.stat.Hp += healthUp;
            UiManager.Instance.DisplayHeal(atker.transform, healthUp);

        }
    }


}
