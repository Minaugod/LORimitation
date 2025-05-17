using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using TMPro;
public class Ui_KeyPageResist : MonoBehaviour
{
    public DamageType damageType;

    public DiceType attackType;

    public TMP_Text resistText;

    public void SetResistText(KeyPage keyPage)
    {
        Resist resist = keyPage.GetResist(damageType, attackType);

        resistText.text = ResourceManager.Instance.resistResource.FindResistText(resist);
    }



}
