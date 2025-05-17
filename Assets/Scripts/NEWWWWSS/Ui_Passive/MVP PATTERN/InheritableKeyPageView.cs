using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class InheritableKeyPageView : MonoBehaviour
{
    bool isInherited = false;

    private KeyPage keyPage;

    private KeyPage settingKeyPage;

    [SerializeField] private GameObject inheritedPanel;

    [SerializeField] private GameObject otherInheritedPanel;

    [SerializeField] private GameObject unInheritPanel;

    [SerializeField] private Ui_KeyPageShapeSetter keyPageShapeSetter;

    public Subject<KeyPage> inheritSubject = new Subject<KeyPage>();

    public Subject<KeyPage> unInheritSubject = new Subject<KeyPage>();


    public void InitSettingKeyPage(KeyPage settingKeyPage)
    {
        this.settingKeyPage = settingKeyPage;
    }

    public void OnClickKeyPage()
    {
        if (isInherited)
        {
            if (settingKeyPage == keyPage.inheritorKeyPage)
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


    public void InitView(KeyPage keyPage)
    {
        this.keyPage = keyPage;

        keyPageShapeSetter.SetKeyPage(keyPage);

        otherInheritedPanel.SetActive(false);
        inheritedPanel.SetActive(false);

        gameObject.SetActive(true);


        if (keyPage.inheritorKeyPage != null)
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
        }

    }


    public void DisableView()
    {
        gameObject.SetActive(false);
    }


}
