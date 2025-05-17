using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;
using UnityEngine.UI;
public class CounterDiceUi : MonoBehaviour
{
    private IObjectPool<CounterDiceUi> _ManagedPool;

    InBattleDice diceInfo;


    [SerializeField]
    private TMP_Text diceValue;

    [SerializeField]
    private Image diceTypeImage;

    public void SetManagedPool(IObjectPool<CounterDiceUi> pool)
    {
        _ManagedPool = pool;
    }
    public void DestroyDice()
    {
        _ManagedPool.Release(this);
    }

    public void SetEvent(CounterDicePreview counterDicePreview)
    {
        counterDicePreview.onHideDice += HideDice;
        counterDicePreview.onDisplayDice += DisplayDice;
        counterDicePreview.onUsedDice += UsedDice;
    }

    public void InitDice(InBattleDice diceInfo)
    {
        this.diceInfo = diceInfo;

        Dice dice = diceInfo.dice;

        diceValue.text = string.Format("{0}-{1}", dice.diceMin, dice.diceMax);

        diceTypeImage.sprite = ResourceManager.Instance.cardResource.GetDiceSprite(dice.diceType);

    }

    private void HideDice()
    {
        gameObject.SetActive(false);
    }

    private void DisplayDice()
    {
        if(diceInfo != null)
        {
            gameObject.SetActive(true);
        }

    }

    private void UsedDice()
    {
        if (diceInfo != null)
        {
            diceInfo = null;
            _ManagedPool.Release(this);
        }
    }

}
