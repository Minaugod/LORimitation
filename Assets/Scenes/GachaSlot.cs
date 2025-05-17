using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSlot : MonoBehaviour
{
    GachaRoller roller;

    GachaStock stock;

    private void Awake()
    {
        roller = GetComponent<GachaRoller>();
        stock = GetComponent<GachaStock>();
    }


    public void GachaSlotPick()
    {
        List<int> gachaStock = stock.GetGachaStock();

        if(gachaStock.Count > 0)
        {
            int gachaResult = roller.GachaRolling(gachaStock);

            stock.RemoveStock(gachaResult);
        }



    }

    public void SlotShake()
    {
        stock.ShuffleStock();
    }



}
