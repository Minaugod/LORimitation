using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class InheritableKeyPagePresenter : MonoBehaviour
{
    private KeyPage settingKeyPage;

    [SerializeField] private InheritableKeyPageModel model;

    [SerializeField] private InheritableKeyPageView[] views;

    [SerializeField] private InheritedKeyPagePresenter InheritedKeyPagePresenter;

    private void Initialize()
    {
        foreach (var view in views)
        {
            view.inheritSubject
                .AsObservable()
                .Subscribe(keyPage =>
                {
                    model.TryInheritKeyPage(keyPage);
                });

            view.unInheritSubject
                .AsObservable()
                .Subscribe(keyPage =>
                {
                    model.UnInheritKeyPage(keyPage);
                });

        }

        model.onDataChanged += RefreshKeyPageList;
        model.onDataChanged += InheritedKeyPagePresenter.RefreshKeyPageList;
    }
    private void Start()
    {
        Initialize();
    }


    public void RefreshKeyPageList()
    {
        List<KeyPage> AllKeyPages = DataManager.Inst.keyPages;

        List<KeyPage> inheritableKeyPages = new List<KeyPage>();

        foreach (var keyPage in AllKeyPages)
        {

            if (keyPage != this.settingKeyPage)
            {
                if (keyPage.NewGetEquippedLibrarian() == null && keyPage.inheritingKeyPages.Count < 1)
                {
                    inheritableKeyPages.Add(keyPage);

                }
            }
        }


        for (int i = 0; i < views.Length; i++)
        {

            if (inheritableKeyPages.Count > i)
            {
                views[i].InitView(inheritableKeyPages[i]);
            }

            else
            {
                views[i].DisableView();
            }


        }
    }

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

}
