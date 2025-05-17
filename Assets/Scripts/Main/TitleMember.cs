using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
public class TitleMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public CharacterBaseInfo character;

    public event Action<TitleMember> OnCharacterClicked;

    public event Action<bool> OnToggleActive;

    private bool allow;

    public bool IsAllow { get { return allow; } }

    [SerializeField]
    private GameObject allowed;

    [SerializeField]
    private GameObject disAllowed;


    [SerializeField]
    private Image uiBg;

    private Image uiBase;

    [SerializeField]
    private Color selectColor;

    private Color normalColor;

    private bool isClicked = false;


    private void Awake()
    {
        uiBase = GetComponent<Image>();
        normalColor = uiBase.color;
    }

    public void ToggleAllow()
    {

        if (allow)
        {
            allow = false;
            allowed.SetActive(false);
            disAllowed.SetActive(true);

        }

        else
        {
            allow = true;
            allowed.SetActive(true);
            disAllowed.SetActive(false);

        }

        OnToggleActive.Invoke(allow);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (!isClicked)
        {
            uiBase.color = selectColor;
            uiBg.color = selectColor;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            uiBase.color = normalColor;
            uiBg.color = normalColor;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (!isClicked)
        {
            isClicked = true;
            OnCharacterClicked?.Invoke(this);
        }

    }


    public void UnSelectUi()
    {
        isClicked = false;
        uiBase.color = normalColor;
        uiBg.color = normalColor;
    }



}
