using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
public class Stage : MonoBehaviour, IPointerDownHandler
{

    [SerializeField]
    private Sprite stageLogo;

    [SerializeField]
    private string stageName;

    [SerializeField]
    private CharacterBaseInfo[] stageCharacters;

    public event Action<Stage> OnStageClicked;

    public Sprite GetStageLogo() { return stageLogo; }

    public string GetStageName() { return stageName; }
    public CharacterBaseInfo[] GetStageCharacters() { return stageCharacters; }

    public void OnPointerDown(PointerEventData eventData)
    {
        Select();
    }

    public void Select()
    {

        OnStageClicked.Invoke(this);


    }


    public void UnSelect()
    {


    }

}
