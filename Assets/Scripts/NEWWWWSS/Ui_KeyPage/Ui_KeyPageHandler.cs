using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Ui_KeyPageHandler : MonoBehaviour
{

    Ui_KeyPage selectedKeyPage;

    [SerializeField]
    private GameObject keyPageSetPrefab;

    [SerializeField]
    private Transform setTransform;

    [SerializeField]
    private Ui_KeyPageInfo keyPageInfo;

    [SerializeField]
    private Vector3 keyPageInfoOffset;

    private List<Ui_KeyPageSet> ui_KeyPageSets = new List<Ui_KeyPageSet>();


    public UnityEvent<KeyPage> onEquipKeyPage;

    private void Start()
    {
        List<KeyPageSet> keyPageSets = ResourceManager.Instance.keyPageSet;

        foreach (var keyPageSet in keyPageSets)
        {
            Ui_KeyPageSet ui_KeyPageSet = Instantiate(keyPageSetPrefab, setTransform).GetComponent<Ui_KeyPageSet>();
            ui_KeyPageSet.InitKetPageSet(keyPageSet);

            SetEvents(ui_KeyPageSet);

            ui_KeyPageSets.Add(ui_KeyPageSet);
        }
    }

    void SetEvents(Ui_KeyPageSet ui_KeyPageSet)
    {
        foreach (var ui_KeyPage in ui_KeyPageSet.GetUiKeyPages())
        {
            ui_KeyPage.onKeyPageHover.AddListener(HoverKeyPage);
            ui_KeyPage.onKeyPageExit.AddListener(HideKeyPageInfo);
            ui_KeyPage.onKeyPageSelect.AddListener(SelectedKeyPage);
            ui_KeyPage.onKeyPageEquip.AddListener(EquipKeyPage);
        }
    }

    public void InitInfo(Librarian librarian)
    {
        foreach (var keyPageSet in ui_KeyPageSets)
        {
            keyPageSet.InitSettingLibrarian(librarian);
        }
    }

    void EquipKeyPage()
    {
        onEquipKeyPage?.Invoke(selectedKeyPage.keyPage);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UnSelectKeyPage();
        }
    }

    void SelectedKeyPage(Ui_KeyPage ui_KeyPage)
    {
        PopupManager.Instance.isCloseable = false;

        if (selectedKeyPage != null) selectedKeyPage.UnSelectKeyPage();

        selectedKeyPage = ui_KeyPage;

        selectedKeyPage.SelectKeyPage();

        ShowKeyPageInfo(ui_KeyPage);

    }

    void UnSelectKeyPage()
    {
        if (selectedKeyPage != null)
        {
            selectedKeyPage.UnSelectKeyPage();

            selectedKeyPage = null;

            HideKeyPageInfo();

            PopupManager.Instance.isCloseable = true;
        }
    }

    void HoverKeyPage(Ui_KeyPage ui_KeyPage)
    {
        if (selectedKeyPage != null)
            return;

        ShowKeyPageInfo(ui_KeyPage);
    }

    void ShowKeyPageInfo(Ui_KeyPage ui_KeyPage)
    {
        


        keyPageInfo.InitInfo(ui_KeyPage.keyPage);

        keyPageInfo.transform.position = ui_KeyPage.transform.position + keyPageInfoOffset;

        Vector3 keyPagePos = Camera.main.WorldToViewportPoint(keyPageInfo.transform.position);
        if (keyPagePos.y < 0.45f) { keyPagePos.y = 0.45f; }
        if (keyPagePos.y > 0.55f) { keyPagePos.y = 0.55f; }
        keyPagePos = Camera.main.ViewportToWorldPoint(keyPagePos);

        keyPageInfo.transform.position = keyPagePos;

        keyPageInfo.gameObject.SetActive(true);
    }

    void HideKeyPageInfo()
    {
        if (selectedKeyPage != null)
            return;

        keyPageInfo.gameObject.SetActive(false);
    }
}
