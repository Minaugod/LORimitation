using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitlePassiveInfo : MonoBehaviour
{

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text descText;


    public void EquipPassive(PassiveEffect passive)
    {
        nameText.text = passive.passiveName;

        descText.text = passive.passiveDesc;

    }

    public void UnEquip()
    {
        nameText.text = "";

        descText.text = "";
    }


}
