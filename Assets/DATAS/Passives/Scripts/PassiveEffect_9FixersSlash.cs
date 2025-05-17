using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "9���ذ��������", menuName = "Custom/PassiveEffects/9FixersSlash")]
public class PassiveEffect_9FixersSlash : PassiveEffect
{

    public int upgradeValue;
    public override void EquipEffect(Character character)
    {



        character.behaviour.onDiceRoll += Effect;


    }

    private void Effect(Character character, EnumTypes.DiceType type)
    {
        // 50% Ȯ���� ���� ���� ����
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {
            character.stat.diceBonusValueDic[EnumTypes.DiceType.Slash] += 1;
        }
    }



}
