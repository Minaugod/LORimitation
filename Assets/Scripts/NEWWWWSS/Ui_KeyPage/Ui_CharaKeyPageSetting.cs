using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Ui_CharaKeyPageSetting : MonoBehaviour
{


    Librarian currentLibrarian;

    [SerializeField]
    private Ui_CustomizeCategory ui_Category;

    [SerializeField]
    private Animator titleAnim;

    [SerializeField]
    private TMP_Text titleNameText;


    [SerializeField]
    private Ui_KeyPageHandler ui_KeyPageHandler;

    public event System.Action onDataChanged;

    public void SetKeyPageSettingInfo(Librarian librarian)
    {
        currentLibrarian = librarian;

        ui_KeyPageHandler.InitInfo(librarian);

        EventSystem.current.SetSelectedGameObject(gameObject);
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OpenSettingPanel()
    {
        if (gameObject.activeSelf) return;

        gameObject.SetActive(true);

        titleNameText.text = "«ŸΩ… √•¿Â";

        titleAnim.Play("TitleChanged");

        ui_Category.SelectCategory();

    }
    public void ClosePanel()
    {
        ui_Category.DeSelectCategory();

        gameObject.SetActive(false);
    }

}
