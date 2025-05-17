using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class Ui_InheritedKeyPageList : MonoBehaviour
{

    [SerializeField]
    public List<Ui_InheritedKeyPage> ui_InheritedKeyPages = new List<Ui_InheritedKeyPage>();

    public List<Ui_InheritedKeyPage> Ui_KeyPages { get { return ui_InheritedKeyPages; } }

    KeyPage settingKeyPage;
    public void InitKeyPageList(KeyPage settingKeyPage)
    {
        this.settingKeyPage = settingKeyPage;

        RefreshKeyPageList();
    }

    public void RefreshKeyPageList()
    {
        for (int i = 0; i < ui_InheritedKeyPages.Count; ++i)
        {
            if (settingKeyPage.inheritingKeyPages.Count > i)
            {
                ui_InheritedKeyPages[i].InitKeyPage(settingKeyPage.inheritingKeyPages[i]);
            }

            else
            {
                ui_InheritedKeyPages[i].DisableKeyPage();
            }
        }
    }




}
