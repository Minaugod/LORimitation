using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ui_TaskPanel : MonoBehaviour
{
    Ui_CharaProfile currentProfile;

    Librarian librarian;

    [SerializeField]
    private Ui_CharaCardPool ui_CardInfo;

    [SerializeField]
    private Ui_CharaKeyPageInfo ui_KeyPageInfo;



    [SerializeField]
    private Ui_CharaCardSetting ui_CardSetting;

    [SerializeField]
    private Ui_CharaKeyPageSetting ui_KeyPageSetting;

    [SerializeField]
    private GameObject sepirBg;

    [SerializeField]
    private GameObject taskBg;

    public void OpenTaskBg()
    {
        taskBg.SetActive(true);
        sepirBg.SetActive(false);
    }

    public void SelectCharaProfile(Ui_CharaProfile profile)
    {
        if (currentProfile != null) currentProfile.DeSelectProfile();



        currentProfile = profile;

        OpenCustomizePanel(profile.librarian);
    }

    public void OpenCustomizePanel(Librarian librarian)
    {
        this.librarian = librarian;

        bool isOpenedPanel = gameObject.activeSelf;

        gameObject.SetActive(true);

        OpenTaskBg();

        RefreshData();

        ui_CardSetting.SetCardSettingInfo(librarian);


        if (!isOpenedPanel)
        {
            ChangeToCardSettingPanel();
        }

    }

    private void Start()
    {
        ui_CardSetting.onDataChanged += RefreshData;
    }

    void RefreshData()
    {
        ui_CardInfo.RefreshCardPool(librarian.keyPage);
        ui_KeyPageInfo.RefreshInfo(librarian);
    }


    public void ChangeToCardSettingPanel()
    {
        ui_KeyPageSetting.ClosePanel();
        ui_KeyPageInfo.DeSelectSetting();


        ui_CardInfo.SelectSetting();
        ui_CardSetting.OpenSettingPanel();
    }

    public void ChangeToPageSettingPanel()
    {
        ui_CardSetting.ClosePanel();
        ui_CardInfo.DeSelectSetting();

        ui_KeyPageSetting.OpenSettingPanel();
        ui_KeyPageInfo.SelectSetting();
    }

    public void ClosePanel()
    {
        librarian = null;
        currentProfile = null;
        gameObject.SetActive(false);

        taskBg.SetActive(false);
        sepirBg.SetActive(true);
    }


}
