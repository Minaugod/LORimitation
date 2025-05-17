using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InfoPanel_Passive : MonoBehaviour
{
    [SerializeField]
    private TMP_Text passiveName;
    [SerializeField]
    private TMP_Text passiveDesc;


    public void  SetPassiveDesc(string name, string desc)
    {
        passiveName.text = name;
        passiveDesc.text = desc;
    }

}
