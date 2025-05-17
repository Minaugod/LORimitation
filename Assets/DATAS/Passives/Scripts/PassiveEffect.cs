using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public abstract class PassiveEffect : ScriptableObject
{

    public EnumTypes.Rarity rarity;

    public int passiveCost;

    public string passiveName;

    [TextArea]
    public string passiveDesc;

    public abstract void EquipEffect(Character character);




}
