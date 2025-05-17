using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
public class ConfirmDialog : MonoBehaviour
{

    [SerializeField] private GameObject dialogPanel;

    [SerializeField] private TMP_Text messageText;

    private Action onConfirm;

    private Action onCancel;


    public void Show(string message, Action onConfirm, Action onCancel = null)
    {
        dialogPanel.SetActive(true);

        messageText.text = message;

        this.onConfirm = onConfirm;

        this.onCancel = onCancel;
    }


    public void OnConfirm(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            onConfirm?.Invoke();
            Close();
        }

 
    }

    public void OnCancel()
    {
        onCancel?.Invoke();
        Close();
    }

    void Close()
    {
        dialogPanel.SetActive(false);

        onCancel = null;
        onConfirm = null;
    }



}
