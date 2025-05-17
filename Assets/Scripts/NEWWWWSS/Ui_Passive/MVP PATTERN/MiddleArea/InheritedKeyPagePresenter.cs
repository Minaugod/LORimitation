using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class InheritedKeyPagePresenter : MonoBehaviour
{

    private KeyPage settingKeyPage;

    [SerializeField] private InheritedKeyPageModel model;

    [SerializeField] private InheritedKeyPageView[] views;


    // OtherPresenters
    [SerializeField] private InheritableKeyPagePresenter InheritableKeyPagePresenter;

    public void InitPresenter(KeyPage settingKeyPage)
    {
        this.settingKeyPage = settingKeyPage;

        model.InitSettingKeyPage(settingKeyPage);

        foreach(var view in views)
        {
            view.InitSettingKeyPage(settingKeyPage);
        }

        RefreshKeyPageList();

    }
    private void Initialize()
    {
        foreach (var view in views)
        {
            view.inheritSubject
                .AsObservable()
                .Subscribe(passive =>
                {
                    model.ToggleInheritPassive(passive);
                });

            

        }

        model.onDataChanged += RefreshKeyPageList;
        model.onDataChanged += InheritableKeyPagePresenter.RefreshKeyPageList;
    }
    private void Start()
    {
        Initialize();
    }

    public void RefreshKeyPageList()
    {

        for (int i = 0; i < views.Length; i++)
        {

            List<InheritingKeyPage> inheritingKeyPages = settingKeyPage.InheritingKeyPages;
            
            if(inheritingKeyPages.Count > i)
            {
                views[i].InitView(inheritingKeyPages[i]);
            }

            else
            {
                views[i].DisableView();
            }

        }

    }


}
