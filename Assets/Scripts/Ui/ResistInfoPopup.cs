using EnumTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResistInfoPopup : MonoBehaviour
{
    [SerializeField]
    protected string defaultTypeText;

    [SerializeField]
    protected string text;

    [SerializeField]
    protected Image resistImg;

    [SerializeField]
    protected DiceType resistType;


    public abstract void ResistInit(Resist resist);

    public void ResistHover()
    {
        
        UiManager.Instance.infoPopup.DisplayPopup(text, transform.position + new Vector3(175, 2));

    }

    public void HoverExit()
    {
        UiManager.Instance.infoPopup.DisablePopup();
    }


}
