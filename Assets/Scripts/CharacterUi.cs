using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUi : MonoBehaviour
{

    private RencounterHandler rencounter;
    public RencounterHandler Rencounter { get { return rencounter; } }

    private CounterDicePreview counterDicePreview;
    public CounterDicePreview CounterDicePreview { get { return counterDicePreview; } }

    public void Init(Character character)
    {

        rencounter = UiManager.Instance.AddRencounter(character);

        counterDicePreview = UiManager.Instance.AddCounterDicePreview(character);

        UiManager.Instance.AddStatBar(character);

        UiManager.Instance.AddEmotionLevel(character);
    }

    public void OnDisable()
    {
        if (rencounter != null) rencounter.gameObject.SetActive(false);


    }



}
    