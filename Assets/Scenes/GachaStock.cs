using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaStock : MonoBehaviour
{
    [SerializeField]
    private List<int> gachaStock = new List<int>();


    public List<int> GetGachaStock()
    {
        return gachaStock;
    }

    public void RemoveStock(int stock)
    {
        gachaStock.Remove(stock);
    }

    public void ShuffleStock()
    {
        int random1, random2;
        int temp;

        for (int i = 0; i < gachaStock.Count; ++i)
        {
            random1 = Random.Range(0, gachaStock.Count);
            random2 = Random.Range(0, gachaStock.Count);

            temp = gachaStock[random1];
            gachaStock[random1] = gachaStock[random2];
            gachaStock[random2] = temp;
        }
    }
}
