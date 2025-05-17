using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Pool;
public class RencounterDice : MonoBehaviour
{

    private IObjectPool<RencounterDice> _ManagedPool;
    public Dice diceData { get; private set; }

    [SerializeField]
    private Image diceImg;

    [SerializeField]
    private TMP_Text diceValue;





    public void InitDice(Dice diceData)
    {
        this.diceData = diceData;

        diceImg.sprite = ResourceManager.Instance.cardResource.GetDiceSprite(diceData.diceType);
    }

    
    public void SetEvent(Rencounter handler)
    {
        handler.onEndCard += DestroyDice;
    }

    public void SetManagedPool(IObjectPool<RencounterDice> pool)
    {
        _ManagedPool = pool;
    }
    public void DestroyDice()
    {
        if (!gameObject.activeSelf) return;
        _ManagedPool.Release(this);
    }


}
