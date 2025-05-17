using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;
public class TitleEnemyMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public CharacterBaseInfo character;

    public event Action<TitleEnemyMember> OnCharacterClicked;

    private bool allow;

    public bool IsAllow { get { return allow; } }


    [SerializeField]
    private Image uiBg;

    private Image uiBase;

    [SerializeField]
    private TMP_Text memberName;

    [SerializeField]
    private Image memberThumb;

    [SerializeField]
    private Color selectColor;

    private Color normalColor;

    private bool isClicked = false;


    private void Awake()
    {
        uiBase = GetComponent<Image>();
        normalColor = uiBase.color;

        LockMember();
    }

    public void LockMember()
    {
        character = null;
        uiBg.gameObject.SetActive(false);
    }
    public void UnlockMember(CharacterBaseInfo character)
    {
        this.character = character;

        memberName.text = character.name;

        memberThumb.sprite = character.thumb;

        uiBg.gameObject.SetActive(true);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {

        if (!isClicked && character != null)
        {
            uiBase.color = selectColor;
            uiBg.color = selectColor;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked && character != null)
        {
            uiBase.color = normalColor;
            uiBg.color = normalColor;

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (character != null)
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
