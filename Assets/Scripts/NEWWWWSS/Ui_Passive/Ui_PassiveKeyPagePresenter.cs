using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class Ui_PassiveKeyPagePresenter : MonoBehaviour
{

    //private KeyPage settingKeyPage;

    public ReactiveProperty<KeyPage> settingKeyPage;

    [SerializeField] private Ui_InheritedKeyPageList inheritedKeyPageList;

    [SerializeField] private Ui_PassiveKeyPageList passiveKeyPageList;


    public void InitPresenter(KeyPage settingKeyPage)
    {
        this.settingKeyPage.Value = settingKeyPage;

        inheritedKeyPageList.InitKeyPageList(settingKeyPage);
        passiveKeyPageList.InitSettingKeyPage(settingKeyPage);

    }
    private void Start()
    {
        passiveKeyPageList.keyPageSubject
            .AsObservable()
            .Subscribe(keyPage =>
            {
                SubscribeKeyPage(keyPage);
            });


        foreach(var keyPage in inheritedKeyPageList.Ui_KeyPages)
        {
            foreach(var passive in keyPage.Ui_Passives)
            {
                passive.SubEvent(settingKeyPage);
            }
        }

    }

    void SubscribeKeyPage(Ui_PassiveKeyPage passiveKeyPage)
    {

        passiveKeyPage.inheritSubject
            .AsObservable()
            .Subscribe(keyPage =>
            {
                TryInheritKeyPage(keyPage);
            });


        passiveKeyPage.unInheritSubject
            .AsObservable()
            .Subscribe(keyPage =>
            {
                UnInheritKeyPage(keyPage);
            });
    }

    void TryInheritKeyPage(KeyPage keyPage)
    {
        // 조건 만족시 
        if (true)
        {

            settingKeyPage.Value.AddInheritKeyPage(keyPage);

            RefreshAllView();
        }

    }

    void UnInheritKeyPage(KeyPage keyPage)
    {
        keyPage.UnInheritCurrentInheritor();

        RefreshAllView();
    }

    void RefreshAllView()
    {
        inheritedKeyPageList.RefreshKeyPageList();
        passiveKeyPageList.RefreshPassiveList();
    }


}
