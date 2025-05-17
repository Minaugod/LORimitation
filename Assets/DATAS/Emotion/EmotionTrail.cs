using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EmotionTrail : MonoBehaviour
{


    [SerializeField]
    private AnimationCurve verticalCurve;
    [SerializeField]
    private AnimationCurve horizonCurve;

    TrailRenderer trail;

    [SerializeField]
    Color positiveColor;

    [SerializeField]
    Color negativeColor;

    private IObjectPool<EmotionTrail> _ManagedPool;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    public void SetManagedPool(IObjectPool<EmotionTrail> pool)
    {
        _ManagedPool = pool;
    }

    public void DestroyTrail()
    {
        _ManagedPool.Release(this);
    }

    public void GetPositiveCoin(Vector3 startPos, Vector3 endPos)
    {


        trail.startColor = positiveColor;
        trail.endColor = positiveColor;
        StartCoroutine(DisplayTrail(startPos, endPos));

    }

    public void GetNegativeCoin(Vector3 startPos, Vector3 endPos)
    {


        trail.startColor = negativeColor;
        trail.endColor = negativeColor;
        StartCoroutine(DisplayTrail(startPos, endPos));

    }

    private Vector3 CalcStartPos(Vector3 pos)
    {
        Vector3 mainPos = Camera.main.WorldToViewportPoint(pos);

        Vector3 resultPos = UiManager.Instance.uiCamera.ViewportToWorldPoint(mainPos);

        return resultPos;
    }

    private IEnumerator DisplayTrail(Vector3 startPos, Vector3 endPos)
    {
        startPos = CalcStartPos(startPos);

        trail.transform.position = startPos;

        float duration = 0.5f;
        float time = 0.0f;


        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            float vertical = verticalCurve.Evaluate(linearT);

            float horizon = horizonCurve.Evaluate(linearT);

            float height = Mathf.Lerp(0, 4f, vertical);

            trail.transform.position = Vector2.Lerp(startPos, endPos, horizon) + new Vector2(0, height);

            yield return null;
        }

        trail.Clear();
        DestroyTrail();

        yield return null;
    }

}
