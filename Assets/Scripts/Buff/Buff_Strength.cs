using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Strength : BuffUi
{
    

    int duration = 1;

    private void Start()
    {
        target.stat.ApplyBuff.AddListener(() =>
        {

            if(duration == 0)
            {
                Destroy(gameObject);
            }

            duration--;

        });


        target.behaviour.onDiceRoll += ApplyBuff;
     }

    private void ApplyBuff(Character target, EnumTypes.DiceType type)
    {
        if(duration == 0)
        {
            target.stat.diceBonusValueDic[EnumTypes.DiceType.Slash] += target.stat.buff.strength;
            target.stat.diceBonusValueDic[EnumTypes.DiceType.Pierce] += target.stat.buff.strength;
            target.stat.diceBonusValueDic[EnumTypes.DiceType.Blunt] += target.stat.buff.strength;
        }

    }

    private void Update()
    {

        if(target != null && duration > 0)
        {
            value.text = target.stat.nextTurnBuff.strength.ToString();
        }

    }
    
}
