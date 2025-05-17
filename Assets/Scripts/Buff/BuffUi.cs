using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffUi : MonoBehaviour
{
    [SerializeField]
    protected Image icon;

    [SerializeField]
    [TextArea]
    protected string buffDesc;

    [SerializeField]
    protected TMPro.TMP_Text value;

    protected Character target;

    public void HoverUi()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y -= 80;
        UiManager.Instance.infoPopup.DisplayPopup(buffDesc, pos);
    }

    public void ExitUi()
    {
        UiManager.Instance.infoPopup.DisablePopup();
    }

    public void InitTarget(Character character)
    {
        target = character;
    }

}
