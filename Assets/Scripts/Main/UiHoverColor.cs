using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiHoverColor : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;

    private Color normalColor;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        normalColor = image.color;
    }

    public void HoverUi()
    {
        image.color = hoverColor;
    }

    public void ExitUi()
    {
        image.color = normalColor;
    }
}
