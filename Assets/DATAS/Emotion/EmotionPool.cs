using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class EmotionPool : MonoBehaviour
{

    private IObjectPool<EmotionTrail> _Pool;

    [SerializeField]
    private GameObject trailPrefab;

    private static EmotionPool instance;

    public static EmotionPool Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        _Pool = new ObjectPool<EmotionTrail>(CreateTrail, OnGetTrail, OnReleaseTrail, OnDestroyTrail, maxSize: 10);

    }


    private EmotionTrail CreateTrail()
    {
        EmotionTrail trail = Instantiate(trailPrefab).GetComponent<EmotionTrail>();
        trail.SetManagedPool(_Pool);
        return trail;
    }

    public EmotionTrail GetTrail()
    {
        return _Pool.Get();
    }

    private void OnGetTrail(EmotionTrail trail)
    {
        trail.gameObject.SetActive(true);
    }

    private void OnReleaseTrail(EmotionTrail trail)
    {
        trail.gameObject.SetActive(false);
    }

    private void OnDestroyTrail(EmotionTrail trail)
    {
        Destroy(trail.gameObject);
    }


}
