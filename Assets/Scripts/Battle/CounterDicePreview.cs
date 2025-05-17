using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;
public class CounterDicePreview : MonoBehaviour
{
    private IObjectPool<CounterDiceUi> _Pool;

    [SerializeField]
    private GameObject counterDicePrefab;

    [SerializeField]
    private Vector3 offset;

    Character character;


    public event Action onHideDice;

    public event Action onUsedDice;

    public event Action onDisplayDice;

    public void InitCharacter(Character character)
    {
        this.character = character;
    }

    private void Awake()
    {
        _Pool = new ObjectPool<CounterDiceUi>(CreateDice, OnGetDice, OnReleaseDice, OnDestroyDice, maxSize: 10);
    }


    public void HideCounterDice()
    {
        onHideDice?.Invoke();
    }


    public void DisplayCounterDice()
    {
        onDisplayDice?.Invoke();
    }

    public void UsedCounterDice()
    {
        onUsedDice?.Invoke();
    }


    private CounterDiceUi CreateDice()
    {

        CounterDiceUi dice = Instantiate(counterDicePrefab).GetComponent<CounterDiceUi>();

        dice.transform.SetParent(transform);

        dice.SetManagedPool(_Pool);
        dice.SetEvent(this);

        return dice;
    }

    public CounterDiceUi GetDice()
    {
        return _Pool.Get();
    }

    private void OnGetDice(CounterDiceUi dice)
    {
        dice.gameObject.SetActive(false);
    }

    private void OnReleaseDice(CounterDiceUi dice)
    {
        dice.gameObject.SetActive(false);
    }

    private void OnDestroyDice(CounterDiceUi dice)
    {
        Destroy(dice.gameObject);
    }


    private void Update()
    {
        if (character != null)
        {
            transform.position = character.transform.position + offset;

        }

    }
}
