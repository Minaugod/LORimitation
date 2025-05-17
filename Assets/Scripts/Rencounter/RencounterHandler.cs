using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RencounterHandler : MonoBehaviour
{


    private Character character;

    [SerializeField]
    private Rencounter leftRencounter;

    [SerializeField]
    private Rencounter rightRencounter;

    public event Action<int> onNextRencounter;



    public event Action onNxtRencounter;



    public event Action<int> onDiceResult;

    public event Action onLoseClashing;

    public event Action onEndCard;

    public event Action<BattleEffectAlert> onBattleEffect;

    [SerializeField]
    private Vector3 offset;

    bool isFollowing;

    public void InitTarget(Character character)
    {
        this.character = character;

        character.stat.onStaggered.AddListener(() =>
        {

            EndCard();

        });
    }

    private IEnumerator RencounterFollow()
    {

        isFollowing = true;

        while (isFollowing)
        {
            Vector3 pos = character.transform.position + offset;
            pos.z = 0;

            transform.position = pos;
            yield return null;
        }


        yield return null;
    }

    public void SetRencounter(InBattleAction battleAction, Character target)
    {
        StartCoroutine(RencounterFollow());
        //left
        if (character.transform.position.x - target.transform.position.x < 0)
        {
            leftRencounter.InitRencounter(battleAction);
        }
        //right
        else
        {
            rightRencounter.InitRencounter(battleAction);
        }
    }


    public void SetLeftRencounter(CardData card)
    {
        StartCoroutine(RencounterFollow());
        leftRencounter?.SetRencounter(card);
    }


    public void SetRightRencounter(CardData card)
    {
        StartCoroutine(RencounterFollow());
        rightRencounter?.SetRencounter(card);
    }

    public void NextRencounter(int index)
    {
        onNextRencounter?.Invoke(index);
    }

    public void NxtRencounter()
    {
        onNxtRencounter?.Invoke();
    }

    public void DiceResult(int value)
    {
        onDiceResult?.Invoke(value);
    }

    public void BattleEffect(BattleEffectAlert alert)
    {
        onBattleEffect?.Invoke(alert);
    }

    public void EndCard()
    {
        isFollowing = false;
        onEndCard?.Invoke();
    }
    public void LoseClash()
    {

        onLoseClashing?.Invoke();
    }


}
