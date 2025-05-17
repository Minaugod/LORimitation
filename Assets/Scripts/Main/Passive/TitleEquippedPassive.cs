using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TitleEquippedPassive : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text descText;

    [SerializeField]
    private PassiveEffect passive;

    [SerializeField]
    private GameObject unEquipObj;

    [SerializeField]
    private GameObject emptyObj;

    public event Action<PassiveEffect> OnPassiveClicked;

    public void InitPassiveInfo(PassiveEffect passive)
    {
        nameText.text = passive.passiveName;

        descText.text = passive.passiveDesc;

        this.passive = passive;

        emptyObj.SetActive(false);

    }

    public void HoverUi()
    {

        if(passive != null)
        {
            unEquipObj.SetActive(true);
        }

    }

    public void ExitUi()
    {
        unEquipObj.SetActive(false);
    }


    public void UnEquipPassive()
    {
        passive = null;
        nameText.text = "";
        descText.text = "";
        emptyObj.SetActive(true);
        unEquipObj.SetActive(false);
    }

    public void ClickPassive()
    {
        if(passive != null)
        {
            OnPassiveClicked?.Invoke(passive);
            UnEquipPassive();
        }

    }

}
