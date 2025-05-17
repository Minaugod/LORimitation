using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_PassiveSetting : MonoBehaviour
{

    KeyPage settingKeyPage;
    
    [SerializeField]
    private Ui_KeyPageShapeSetter shapeSetter;

    [SerializeField]
    private Ui_PassiveKeyPageList ui_PassiveKeyPageList;


    [SerializeField] private InheritableKeyPagePresenter inheritableKeyPagePresenter;

    [SerializeField] private InheritedKeyPagePresenter inheritedKeyPagePresenter;


    [SerializeField] private Ui_PassiveKeyPagePresenter presenter;

    private List<KeyPage.InheritMemento> inheritMementos = new List<KeyPage.InheritMemento>();

    private void Start()
    {
        StartCoroutine(ShowInfoCor());
    }

    IEnumerator ShowInfoCor()
    {
        yield return new WaitUntil(() => DataManager.Inst.IsDataLoaded());

        OpenPassiveSetting(DataManager.Inst.keyPages[5]);

    }
    public void OpenPassiveSetting(KeyPage settingKeyPage)
    {

        this.settingKeyPage = settingKeyPage;

        inheritableKeyPagePresenter.InitPresenter(settingKeyPage);
        inheritedKeyPagePresenter.InitPresenter(settingKeyPage);
        //presenter.InitPresenter(settingKeyPage);
        //RefreshSettingableKeyPage();

        SaveInheritMementos();

        shapeSetter.SetKeyPage(settingKeyPage);

        gameObject.SetActive(true);

    }

    void SaveInheritMementos()
    {
        inheritMementos.Clear();

        List<KeyPage> AllKeyPages = DataManager.Inst.keyPages;

        foreach(var keyPage in AllKeyPages)
        {
            inheritMementos.Add(keyPage.GetInheritMemento());
        }
    }

    private void RefreshSettingableKeyPage()
    {
        //ui_PassiveKeyPageList.InitSettingKeyPage(settingKeyPage);


    }


}
