using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TitleEquipablePassive : MonoBehaviour
{

    [SerializeField]
    private PassiveEffect passive;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text descText;

    [SerializeField]
    private GameObject equipObj;

    [SerializeField]
    private GameObject unEquipableObj;

    public event Action<PassiveEffect> OnPassiveClicked;

    private bool equipable;

    private void Awake()
    {
        nameText.text = passive.passiveName;

        descText.text = passive.passiveDesc;
    }


    public void HoverUi()
    {

        if (equipable)
        {
            equipObj.SetActive(true);
        }

    }

    public void ExitUi()
    {
        equipObj.SetActive(false);
    }

    public void IsEquipable(List<PassiveEffect> passives)
    {
        foreach(PassiveEffect passive in passives)
        {
            if(this.passive == passive)
            {
                // UNEQUIPABLE;
                equipable = false;
                unEquipableObj.SetActive(true);
                return;
            }

            if(passives.Count == 4)
            {
                // MAX
                equipable = false;


                return;
            }
        }

        equipable = true;
        unEquipableObj.SetActive(false);
    }

    public void ClickPassive()
    {
        if (equipable)
        {
            OnPassiveClicked?.Invoke(passive);
        }

    }


}
