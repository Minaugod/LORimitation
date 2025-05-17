using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ui_HoverColorTool : MonoBehaviour
{
    public Image[] changingImage;

    public TMP_Text[] changingText;

    public Color changingColor;

    List<Color> originalImageColor = new List<Color>();

    List<Color> originalTextColor = new List<Color>();

    bool isSelected = false;

    public void HoverUi()
    {
        if (!isSelected)
        {
            ChangeToSelectedColor();
        }

    }

    public void ExitUi()
    {
        if (!isSelected)
        {
            ChangeToOriginalColor();
        }
       

    }

    public void SelectedUi()
    {
        if (!isSelected)
        {
            isSelected = true;

            if(originalImageColor.Count > 0 || originalTextColor.Count > 0)
                ChangeToOriginalColor();

            ChangeToSelectedColor();
        }

    }
    public void UnSelectUi()
    {
        isSelected = false;

        if (originalImageColor.Count > 0 || originalTextColor.Count > 0)
            ChangeToOriginalColor();

    }

    void ChangeToSelectedColor()
    {
        foreach (Image image in changingImage)
        {
            image.color = changingColor;
        }

        foreach (TMP_Text text in changingText)
        {
            text.color = changingColor;
        }
    }

    private void Awake()
    {
        foreach (Image image in changingImage)
        {
            originalImageColor.Add(image.color);

            //image.color = changingColor;
        }

        foreach (TMP_Text text in changingText)
        {
            originalTextColor.Add(text.color);

            //text.color = changingColor;
        }
    }

    private void OnDisable()
    {
        ChangeToOriginalColor();
    }

    void ChangeToOriginalColor()
    {
        for(int i = 0; i < changingImage.Length; i++)
        {
            changingImage[i].color = originalImageColor[i];

        }

        for (int i = 0; i < changingText.Length; i++)
        {
            changingText[i].color = originalTextColor[i];

        }
    }


}
