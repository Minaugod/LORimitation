using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUiManager : MonoBehaviour
{

    [SerializeField] private ConfirmDialog confirmDialog;

    public void ShowConfirmDialog(string message, System.Action onConfirm, System.Action onCancel)
    {
        confirmDialog.Show(message, onConfirm, onCancel);
    }






    private static GlobalUiManager instance;

    public static GlobalUiManager Inst
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }

    }
}
