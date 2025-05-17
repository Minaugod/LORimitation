using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class Ui_InheritablePassive : MonoBehaviour
{

    [SerializeField] private Ui_PassiveShapeSetter passiveShapeSetter;

    private KeyPage settingKeyPage;

    public void SubEvent(ReactiveProperty<KeyPage> settingKeyPage)
    {
        settingKeyPage.Subscribe(keyPage =>
        {
            this.settingKeyPage = keyPage;
        });
    }

    public void InitPassive(InheritingKeyPage.InheritablePassive inheritablePassive)
    {

        passiveShapeSetter.InitPassive(inheritablePassive.passiveEffect);

        if (inheritablePassive.isActive)
        {

        }

        gameObject.SetActive(true);

    }


    public void DisablePassive()
    {
        gameObject.SetActive(false);
    }

}
