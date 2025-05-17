using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Buff
{
    public int burn; // 화상

    public int bleed; // 출혈

    public int protection; // 보호

    public int strength; // 힘

    public int endurance; // 인내

    public int haste; // 신속


}

public class BuffManager : MonoBehaviour
{
    public GameObject strengthBuff;

    public GameObject enduranceBuff;

    public GameObject burnBuff;

    public GameObject bleedBuff;

    public GameObject hasteBuff;

    public GameObject protectionBuff;
    
    public void GetStrength(Character target, int value)
    {

        if(target.stat.nextTurnBuff.strength == 0)
        {

            Buff_Strength buff = Instantiate(strengthBuff).GetComponent<Buff_Strength>();

            AddNewBuff(target, buff);

        }


        target.stat.nextTurnBuff.strength += value;


    }
    public void GetEndurance(Character target, int value)
    {

        if (target.stat.nextTurnBuff.endurance == 0)
        {
            Buff_Endurance buff = Instantiate(enduranceBuff).GetComponent<Buff_Endurance>();

            AddNewBuff(target, buff);
        }

        target.stat.nextTurnBuff.endurance += value;
    }

    public void GetBurn(Character target, int value)
    {

        if (target.stat.nextTurnBuff.burn == 0)
        {
            Buff_Burn buff = Instantiate(burnBuff).GetComponent<Buff_Burn>();

            AddNewBuff(target, buff);
        }

        target.stat.nextTurnBuff.burn += value;
    }

    public void GetBleed(Character target, int value)
    {

        if(target.stat.nextTurnBuff.bleed == 0)
        {
            Buff_Bleed buff = Instantiate(bleedBuff).GetComponent<Buff_Bleed>();

            AddNewBuff(target, buff);
        }

        target.stat.nextTurnBuff.bleed += value;
    }
    public void GetHaste(Character target, int value)
    {

        if (target.stat.nextTurnBuff.haste == 0)
        {
            Buff_Haste buff = Instantiate(hasteBuff).GetComponent<Buff_Haste>();

            AddNewBuff(target, buff);
        }


        target.stat.nextTurnBuff.haste += value;
    }

    public void GetProtection(Character target, int value)
    {

        if (target.stat.nextTurnBuff.protection == 0)
        {
            Buff_Protection buff = Instantiate(protectionBuff).GetComponent<Buff_Protection>();

            AddNewBuff(target, buff);
        }

        target.stat.nextTurnBuff.protection += value;
    }
    
    public void AddNewBuff(Character target, BuffUi buff)
    {
        buff.InitTarget(target);

        target.stat.AddBuff(buff);
    }


    private static BuffManager instance;

    public static BuffManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

}
