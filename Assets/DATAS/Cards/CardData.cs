using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using System;

[System.Serializable]
public class Dice
{
    public DiceType diceType;
    public int diceMin;
    public int diceMax;

    public CharacterState characterMotion;
    public Sprite atkEffect;

    public DiceEffect diceEffect;

    public int DiceRoll()
    {
        int value = 0;

        value = UnityEngine.Random.Range(diceMin, diceMax + 1);

        return value;
    }

    public bool IsMaxValue(Character user, int value)
    {
        int bonusValue = user.stat.diceBonusValueDic[diceType];

        int normalValue = value - bonusValue;

        if (normalValue == diceMax) return true;

        else return false;

    }
    public bool IsMinValue(Character user, int value)
    {
        int bonusValue = user.stat.diceBonusValueDic[diceType];

        int normalValue = value - bonusValue;

        if (normalValue == diceMin) return true;

        else return false;

    }

    public void ApplyEffect(Character user, Character target, DiceUseType type)
    {

        //BuffManager.Instance.GetBurn(user, 2);

        if (type == diceEffect.type)
        {
            BattleEffectAlert alert = BattleEffectAlertPool.Instance.GetAlert();

            alert.InitDiceEffect(user, diceEffect);

            diceEffect.effect.ApplyEffect(user, target, diceEffect.value);


        }


    }
}

[System.Serializable]
public class DiceEffect
{

    public DiceUseType type;

    public DiceUseEffect effect;

    public int value;

    public string desc;

}


[CreateAssetMenu(fileName = "New CardData", menuName = "Custom/CardData")]
public class CardData : ScriptableObject
{
    public string address;

    public Rarity rarity;

    public string cardName;

    public int cardCost;

    public Dice[] dice;

    public Sprite image;

    public CardUseEffect useEffect;

    public void CardUseEffect(Character target)
    {
        if(useEffect != null)
        {
            useEffect.ApplyEffect(target);
        }

    }

}
