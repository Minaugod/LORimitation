using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Pool;
using UniRx;
public class Ui_PassiveKeyPageList : MonoBehaviour
{
    public IObjectPool<Ui_PassiveKeyPage> _Pool { get; private set; }

    public ReactiveProperty<KeyPage> settingKeyPage = new ReactiveProperty<KeyPage>();


    [SerializeField]
    private GameObject passiveKeyPagePrefab;

    [SerializeField]
    private Transform passiveKeyPageTf;

    [SerializeField]
    private Vector3 passiveKeyPageScale;


    public Subject<Ui_PassiveKeyPage> keyPageSubject = new Subject<Ui_PassiveKeyPage>();

    public event System.Action onRefreshList;

    private void Awake()
    {
        _Pool = new ObjectPool<Ui_PassiveKeyPage>(CreateKeyPage, OnGetKeyPage, OnReleaseKeyPage, OnDestroyKeyPage, maxSize: 40);
    }


    public void AddPassiveKeyPage(KeyPage keyPage)
    {

        Ui_PassiveKeyPage ui_PassiveKeyPage = GetKeyPage();

        ui_PassiveKeyPage.InitKeyPage(keyPage);

    }

    public void InitSettingKeyPage(KeyPage keyPage)
    {
        settingKeyPage.Value = keyPage;


        RefreshPassiveList();

    }


    public void RefreshPassiveList()
    {
        onRefreshList?.Invoke();

        List<KeyPage> AllKeyPages = DataManager.Inst.keyPages;

        List<KeyPage> settingableKeyPages = new List<KeyPage>();

        foreach (var keyPage in AllKeyPages)
        {

            if(keyPage != settingKeyPage.Value)
            {
                if (keyPage.NewGetEquippedLibrarian() == null && keyPage.inheritingKeyPages.Count < 1)
                {
                    settingableKeyPages.Add(keyPage);

                    AddPassiveKeyPage(keyPage);

                }
            }


        }


    }


    private Ui_PassiveKeyPage CreateKeyPage()
    {

        Ui_PassiveKeyPage keyPage = Instantiate(passiveKeyPagePrefab).GetComponent<Ui_PassiveKeyPage>();

        keyPage.transform.SetParent(passiveKeyPageTf);

        keyPage.transform.localScale = passiveKeyPageScale;

        keyPage.SetPassiveKeyPage(this);

        //unirxt
        keyPageSubject.OnNext(keyPage);

        return keyPage;
    }

    public Ui_PassiveKeyPage GetKeyPage()
    {
        return _Pool.Get();
    }

    private void OnGetKeyPage(Ui_PassiveKeyPage keyPage)
    {
        keyPage.gameObject.SetActive(true);
    }

    private void OnReleaseKeyPage(Ui_PassiveKeyPage keyPage)
    {
        keyPage.gameObject.SetActive(false);
    }

    private void OnDestroyKeyPage(Ui_PassiveKeyPage keyPage)
    {
        Destroy(keyPage.gameObject);
    }


}
