using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TitleButton : MonoBehaviour
{

    [SerializeField]
    private Image image;

    [SerializeField]
    private Color hoverColor;

    [SerializeField]
    private string buttonName;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TMP_Text desc;

    public void HoverUi()
    {
        desc.text = buttonName;
        image.color = hoverColor;
        animator.SetBool("Hover", true);

    }

    public void ExitUi()
    {
        image.color = Color.white;
        animator.SetBool("Hover", false);
    }



}

