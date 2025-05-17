using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ui_CustomizeCategory : MonoBehaviour
{

    [SerializeField]
    private GameObject selectedObject;

    [SerializeField]
    private TMPro.TMP_Text categoryText;

    [SerializeField]
    private Color selectedColor;


    Color originalColor;

    bool isSelected = false;

    private void Awake()
    {
        originalColor = categoryText.color;
    }

    public void SelectCategory()
    {
        isSelected = true;

        categoryText.color = selectedColor;

        selectedObject.SetActive(true);


    }

    public void DeSelectCategory()
    {
        isSelected = false;

        categoryText.color = originalColor;

        selectedObject.SetActive(false);
    }

    public void HoverUi()
    {
        if(!isSelected)
            categoryText.color = selectedColor;

    }

    public void ExitUi()
    {
        if (!isSelected)
            categoryText.color = originalColor;

    }


}
