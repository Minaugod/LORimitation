using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.EventSystems;
using UniRx;
public class Ui_PassiveKeyPage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    bool isInherited = false;

    KeyPage keyPage;

    KeyPage settingKeyPage;

    [SerializeField]
    private GameObject inheritedPanel;

    [SerializeField]
    private GameObject otherInheritedPanel;

    [SerializeField]
    private GameObject unInheritPanel;



    [SerializeField]
    private Ui_KeyPageShapeSetter keyPageShapeSetter;

    private IObjectPool<Ui_PassiveKeyPage> _ManagedPool;



    public Subject<KeyPage> inheritSubject = new Subject<KeyPage>();

    public Subject<KeyPage> unInheritSubject = new Subject<KeyPage>();




    public void SelectKeyPage()
    {

        if (isInherited)
        {
            if(settingKeyPage == keyPage.inheritorKeyPage)
            {
                UnInheritCurrentInheritor();
            }

            else
            {
                GlobalUiManager.Inst.ShowConfirmDialog("상속 해제합니까?", UnInheritCurrentInheritor, null);
            }
        }

        else
        {

            inheritSubject.OnNext(keyPage);

        }

    }

    void UnInheritCurrentInheritor()
    {
        unInheritSubject.OnNext(keyPage);
    }

    public void SetPassiveKeyPage(Ui_PassiveKeyPageList keyPageList)
    {
        _ManagedPool = keyPageList._Pool;

        keyPageList.onRefreshList += ReleaseKeyPage;


        keyPageList.settingKeyPage.Subscribe(keyPage =>
        {
            settingKeyPage = keyPage;
        });

    }

    void CheckEqualInheritor()
    {
        if(keyPage.inheritorKeyPage != null)
        {
            isInherited = true;

            if (settingKeyPage == keyPage.inheritorKeyPage)
            {
                inheritedPanel.SetActive(true);
            }

            else
            {
                otherInheritedPanel.SetActive(true);
            }
        }

        else
        {
            isInherited = false;
            otherInheritedPanel.SetActive(false);
            inheritedPanel.SetActive(false);
        }


    }

    public void ReleaseKeyPage()
    {
        _ManagedPool.Release(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (!isInherited)
        {
            keyPageShapeSetter.HighlightKeyPage();
        }

        if (isInherited)
        {
            unInheritPanel.SetActive(true);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (!isInherited)
        {
            keyPageShapeSetter.UnHighlightKeyPage();
        }


        unInheritPanel.SetActive(false);


    }





    public void InitKeyPage(KeyPage keyPage)
    {
        this.keyPage = keyPage;

        keyPageShapeSetter.SetKeyPage(keyPage);

        CheckEqualInheritor();
    }


}
