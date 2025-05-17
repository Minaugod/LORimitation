using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class InheritableKeyPageModel : MonoBehaviour
{

    private KeyPage settingKeyPage;

    public event System.Action onDataChanged;


    public void InitSettingKeyPage(KeyPage settingKeyPage)
    {
        this.settingKeyPage = settingKeyPage;
    }

    public void TryInheritKeyPage(KeyPage keyPage)
    {

        // 조건 만족시
        if (true)
        {
            settingKeyPage.AddInheritKeyPage(keyPage);
        }

        onDataChanged?.Invoke();
    }

    public void UnInheritKeyPage(KeyPage keyPage)
    {

        keyPage.UnInheritCurrentInheritor();

        onDataChanged?.Invoke();
    }

}
