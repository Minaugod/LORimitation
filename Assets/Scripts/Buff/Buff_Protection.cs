using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Protection : BuffUi
{

    int duration = 1;

    private void Start()
    {
        target.stat.ApplyBuff.AddListener(() =>
        {

            if (duration == 0)
            {
                Destroy(gameObject);
            }

            duration--;

        });
    }

    private void Update()
    {

        if (target != null && duration > 0)
        {
            value.text = target.stat.nextTurnBuff.protection.ToString();
        }

    }
    
}
