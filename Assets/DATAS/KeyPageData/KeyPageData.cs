using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;


[CreateAssetMenu(fileName = "New Data", menuName = "Custom/KeyPageData")]
public class KeyPageData : ScriptableObject
{
    public string address;

    public Rarity rarity;

    public string pageName;

    public GameObject appearance;

    public Sprite iconSprite;
    public Sprite glowIconSprite;


    public Sprite thumbSprite;

    public int hp;
    public int staggerResist;
    public int spdDiceMin;
    public int spdDiceMax;

    public Resist slashDmgResist;
    public Resist pierceDmgResist;
    public Resist bluntDmgResist;

    public Resist slashStaggerResist;
    public Resist pierceStaggerResist;
    public Resist bluntStaggerResist;

    public List<PassiveEffect> passiveEffects = new List<PassiveEffect>();



}
