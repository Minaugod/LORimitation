using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumTypes
{

    public enum CharacterState { Default, Move, Guard, Evade, Damaged, Slash, Pierce, Blunt }
    public enum DiceUseType { None, OnHit, OnWinClash, OnLoseClash }
    public enum DiceType { Slash, Pierce, Blunt, Block, Evade }
    public enum DiceEffectList { None, Heal, Damage, Bleed, Fragile, ChargeLight }
    public enum DamageType { Hp, Stagger }
    public enum Resist { Fatal, Weak, Normal, Endured, Ineffective }
    public enum GameResult { Victory, Defeat }

    public enum Rarity { Paperback, Hardcover, Limited }
}
