using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Yun's Hunch", menuName = "Custom/PassiveEffects/Yun's Hunch")]
public class PassiveEffect_YunsHunch : PassiveEffect
{

    public override void EquipEffect(Character character)
    {
        character.behaviour.onDiceRoll += Effect;


    }

    private void Effect(Character character, EnumTypes.DiceType type)
    {
        int rand = Random.Range(0, 100);

        if (rand < 50)
        {

            character.stat.diceBonusValueDic[EnumTypes.DiceType.Evade] += 2;
        }

    }


}
