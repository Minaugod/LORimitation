using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaRoller : MonoBehaviour
{

    public int GachaRolling(List<int> stock)
    {
        int rand = Random.Range(0, stock.Count);

        return stock[rand];

    }


}
