using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class Ui_KeyPage : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Librarian settingLibrarian;

    public KeyPage keyPage { get; private set; }

    [SerializeField]
    private TMP_Text keyPageName;

    [SerializeField]
    private TMP_Text keyPageNameUnderlay;

    [SerializeField]
    private Image glowIcon;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image[] colorChangingImage;

    [SerializeField]
    private GameObject selectPage;

    // 장착자 관련
    [SerializeField]
    private GameObject portraitObj;

    [SerializeField]
    private RawImage librarianPortrait;




    /// <summary>
    /// 이벤트
    /// </summary>
    public UnityEvent<Ui_KeyPage> onKeyPageSelect;

    public UnityEvent<Ui_KeyPage> onKeyPageHover;

    public UnityEvent onKeyPageExit;

    public UnityEvent onKeyPageEquip;
 

    public void InitSettingLibrarian(Librarian librarian)
    {
        settingLibrarian = librarian;



    }

    public void InitKeyPage(KeyPage keyPage)
    {
        this.keyPage = keyPage;

        keyPageName.text = string.Format("{0}의 책장", keyPage.page.pageName);
        keyPageNameUnderlay.text = string.Format("{0}의 책장", keyPage.page.pageName);



    }

    public void RefreshKeyPageInfo()
    {
        Librarian librarian = keyPage.GetEquippedLibrarian();

        if(librarian != null)
        {
            portraitObj.SetActive(true);
        }

        else
        {
            portraitObj.SetActive(false);
        }
    }

    public void EquipKeyPage()
    {
        onKeyPageEquip?.Invoke();
    }

    public void SelectKeyPage()
    {
        selectPage.SetActive(true);
    }

    public void UnSelectKeyPage()
    {
        selectPage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        onKeyPageSelect?.Invoke(this);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        onKeyPageHover?.Invoke(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        onKeyPageExit?.Invoke();
    }
}
