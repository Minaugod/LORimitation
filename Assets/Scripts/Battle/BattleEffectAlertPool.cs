using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BattleEffectAlertPool : MonoBehaviour
{
    private IObjectPool<BattleEffectAlert> _Pool;

    [SerializeField]
    private GameObject alertPrefab;

    private static BattleEffectAlertPool instance;

    public static BattleEffectAlertPool Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        _Pool = new ObjectPool<BattleEffectAlert>(CreateAlert, OnGetAlert, OnReleaseAlert, OnDestroyAlert, maxSize: 10);

    }


    private BattleEffectAlert CreateAlert()
    {
        BattleEffectAlert effect = Instantiate(alertPrefab).GetComponent<BattleEffectAlert>();
        effect.SetManagedPool(_Pool);
        return effect;
    }

    public BattleEffectAlert GetAlert()
    {
        return _Pool.Get();
    }

    private void OnGetAlert(BattleEffectAlert alert)
    {
        alert.gameObject.SetActive(true);
    }

    private void OnReleaseAlert(BattleEffectAlert alert)
    {
        alert.gameObject.SetActive(false);
    }

    private void OnDestroyAlert(BattleEffectAlert alert)
    {
        Destroy(alert.gameObject);
    }
}
