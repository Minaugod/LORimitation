using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DiceUseEffect : ScriptableObject
{

    public abstract void ApplyEffect(Character user, Character target, int value);

}
