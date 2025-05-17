using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritedKeyPageModel : MonoBehaviour
{
    private KeyPage settingKeyPage;

    public event System.Action onDataChanged;


    public void InitSettingKeyPage(KeyPage settingKeyPage)
    {
        this.settingKeyPage = settingKeyPage;
    }


    public void ToggleInheritPassive(InheritingKeyPage.InheritablePassive inheritablePasive)
    {

        if (inheritablePasive.isActive) inheritablePasive.isActive = false;

        else if(!inheritablePasive.isActive) inheritablePasive.isActive = true;

        onDataChanged?.Invoke();

    }

    public void UnInheritKeyPage(InheritingKeyPage inheritingKeyPage)
    {
        IKeyPage keyPage = inheritingKeyPage.keyPage;

        settingKeyPage.RequestUnInherit(keyPage);

        onDataChanged?.Invoke();
    }

}
