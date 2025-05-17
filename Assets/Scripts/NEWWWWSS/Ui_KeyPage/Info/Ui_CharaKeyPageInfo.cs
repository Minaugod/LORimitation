using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EnumTypes;

public class Ui_CharaKeyPageInfo : MonoBehaviour
{
    KeyPage currentKeyPage;

    public TMP_Text keyPageNameText;

    [SerializeField]
    private TMP_Text underlayText;

    public Image[] colorChangingImage;

    public RawImage portraitImage;

    public TMP_Text hpText;

    public TMP_Text staggerText;

    public TMP_Text spdText;

    public Ui_KeyPageResist[] charaResists;

    [Header("Base Elements")]
    public Color paperbackColor;

    public Color hardcoverColor;

    public Color limitedColor;


    public Color selectColor;

    bool isSelected = false;

    public void HoverUi()
    {
        if (currentKeyPage == null) return;

        if (!isSelected)
        {
            SetImageColor(selectColor);
        }

    }

    public void ExitUi()
    {
        if (currentKeyPage == null) return;

        if (!isSelected)
        {
            SetImageColor(GetRarityColor());
        }
    }

    public void SelectSetting()
    {
        isSelected = true;
        SetImageColor(selectColor);
    }

    public void DeSelectSetting()
    {
        isSelected = false;
        SetImageColor(GetRarityColor());
    }

    public void RefreshInfo(Librarian librarian)
    {
        currentKeyPage = librarian.keyPage;

        portraitImage.texture = librarian.portraitTexture;

        SetPageName(currentKeyPage);

        SetStatus(currentKeyPage);

        SetResist(currentKeyPage);

        if (isSelected) return;
        SetImageColor(GetRarityColor());

    }

    void SetPageName(KeyPage keyPage)
    {
        keyPageNameText.text = string.Format("{0}의 책장", keyPage.page.pageName);
        underlayText.text = string.Format("{0}의 책장", keyPage.page.pageName);

        Color rarityColor = GetRarityColor();

        rarityColor.a = 255;

        underlayText.color = rarityColor;

    }

    void SetStatus(KeyPage keyPage)
    {
        hpText.text = keyPage.page.hp.ToString();

        staggerText.text = keyPage.page.staggerResist.ToString();

        spdText.text = string.Format("{0}~{1}", keyPage.page.spdDiceMin, keyPage.page.spdDiceMax);

    }
    void SetResist(KeyPage keyPage)
    {
        foreach (Ui_KeyPageResist resist in charaResists)
        {
            resist.SetResistText(keyPage);
        }
    }

    void SetImageColor(Color rarityColor)
    {
        foreach (Image image in colorChangingImage)
        {
            image.color = rarityColor;
        }

    }

    Color GetRarityColor()
    {

        switch (currentKeyPage.page.rarity)
        {
            case Rarity.Paperback:
                return paperbackColor;

            case Rarity.Hardcover:
                return hardcoverColor;

            case Rarity.Limited:
                return limitedColor;

            default: return paperbackColor;
        }


    }
}
