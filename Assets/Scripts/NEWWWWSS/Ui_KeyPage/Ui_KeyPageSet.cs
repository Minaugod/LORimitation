using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_KeyPageSet : MonoBehaviour
{
    [SerializeField]
    private GameObject keyPagePrefab;

    [SerializeField]
    private TMP_Text setNameText;

    [SerializeField]
    private Image setIcon;

    [SerializeField]
    private Image setIconGlow;

    private List<Ui_KeyPage> ui_KeyPages = new List<Ui_KeyPage>();

    public void InitKetPageSet(KeyPageSet keyPageSet)
    {
        setNameText.text = keyPageSet.setName;

        foreach (KeyPage keyPage in keyPageSet.keyPages)
        {
            Ui_KeyPage ui_KeyPage = Instantiate(keyPagePrefab, transform).GetComponent<Ui_KeyPage>();
            ui_KeyPage.InitKeyPage(keyPage);

            ui_KeyPages.Add(ui_KeyPage);
        }
    }

    public List<Ui_KeyPage> GetUiKeyPages()
    {
        return ui_KeyPages;
    }

    public void InitSettingLibrarian(Librarian librarian)
    {
        foreach(var ui_KeyPage in ui_KeyPages)
        {
            ui_KeyPage.InitSettingLibrarian(librarian);
        }
    }
    
}
