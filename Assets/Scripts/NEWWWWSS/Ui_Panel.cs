using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Panel : MonoBehaviour
{

    private void OnEnable()
    {
        PopupManager.Instance.OpenedPopup(this);
    }

    private void OnDisable()
    {
        //if (gameObject.activeSelf) gameObject.SetActive(false);
    }


}
