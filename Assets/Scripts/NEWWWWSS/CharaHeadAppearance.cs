using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaHeadAppearance : MonoBehaviour
{
    /*
     
    0 = Def
    1 = Mov
    2 = Damage
    3 = Evade
    4 = Guard
    5 = Slash
    6 = Pene
    7 = Hit

    */

    public GameObject[] heads;

    public GameObject GetHead(int index)
    {
        if (index < heads.Length) return heads[index];
        else return null;
    }


}
