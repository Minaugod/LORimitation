using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "The Art of Defense", menuName = "Custom/PassiveEffects/TheArtOfDefense")]
public class PassiveEffect_TheArtOfDefense : PassiveEffect
{

    public override void EquipEffect(Character character)
    {
        character.behaviour.onDiceRoll += Effect;


    }

    private void Effect(Character character, EnumTypes.DiceType type)
    {
        int rand = Random.Range(0, 100);

        if (rand < 25)
        {

            character.stat.diceBonusValueDic[EnumTypes.DiceType.Block] += 2;
            character.stat.diceBonusValueDic[EnumTypes.DiceType.Evade] += 2;
        }

    }
}
