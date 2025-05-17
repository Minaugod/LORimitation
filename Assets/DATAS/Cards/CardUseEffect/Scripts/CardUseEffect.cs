using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardUseEffect : ScriptableObject
{

    [TextArea]
    public string desc;
    public abstract void ApplyEffect(Character character);
}
