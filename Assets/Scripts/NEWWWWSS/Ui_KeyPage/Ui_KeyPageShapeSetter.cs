using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_KeyPageShapeSetter : MonoBehaviour
{
    EnumTypes.Rarity currentKeyPageRarity;

    [SerializeField]
    private TMP_Text keyPageText;

    [SerializeField]
    private TMP_Text keyPageUnderlayText;

    [SerializeField]
    private Image keyPageIcon;

    [SerializeField]
    private Image keyPageGlowIcon;

    [SerializeField]
    private Image keyPageImage;

    [SerializeField]
    private Image keyPageGlow;

    [SerializeField] private RarityColorData colorData;

    private Color hightlightColor = Color.cyan;

    public void HighlightKeyPage()
    {
        keyPageImage.color = hightlightColor;

        keyPageUnderlayText.color = hightlightColor;

        keyPageGlowIcon.color = hightlightColor;

        keyPageGlow.color = hightlightColor;
    }

    public void UnHighlightKeyPage()
    {
        keyPageImage.color = Color.white;

        Color rarityColor = colorData.GetColor(currentKeyPageRarity);

        keyPageUnderlayText.color = rarityColor;

        keyPageGlowIcon.color = rarityColor;

        keyPageGlow.color = rarityColor;
    }

    public void SetKeyPage(KeyPage keyPage)
    {
        currentKeyPageRarity = keyPage.page.rarity;

        keyPageText.text = string.Format("{0}의 책장", keyPage.page.pageName);

        keyPageUnderlayText.text = string.Format("{0}의 책장", keyPage.page.pageName);

        keyPageIcon.sprite = keyPage.page.iconSprite;

        keyPageGlowIcon.sprite = keyPage.page.glowIconSprite;

        Color rarityColor = colorData.GetColor(currentKeyPageRarity);

        keyPageUnderlayText.color = rarityColor;

        keyPageGlowIcon.color = rarityColor;

        keyPageGlow.color = rarityColor;

    }

}
