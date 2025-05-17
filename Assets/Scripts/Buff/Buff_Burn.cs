using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Burn : BuffUi
{
    

    private void Start()
    {
        target.stat.ApplyBuff.AddListener(() =>
        {

            ApplyBuff();

        });

    }

    public void ApplyBuff()
    {

        target.stat.TakeJustDamage(target.stat.buff.burn);

        target.stat.nextTurnBuff.burn = target.stat.buff.burn / 2;

    }

    private void Update()
    {

        if (target != null && target.stat.buff.burn > 0 || target.stat.nextTurnBuff.burn > 0)
        {
            value.text = target.stat.nextTurnBuff.burn.ToString();
        }

        if (target.stat.buff.burn <= 0 && target.stat.nextTurnBuff.burn <= 0) Destroy(gameObject);

    }
    

}
