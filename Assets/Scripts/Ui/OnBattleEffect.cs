using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnBattleEffect : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private Transform target;

    [SerializeField]
    private Vector3 offset;

    private void Update()
    {
        if(target != null) transform.position = target.position + offset;
    }

    public void Init(Transform target)
    {
        this.target = target;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {

        float time = 0;
        float duration = 0.5f;


        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            canvasGroup.alpha = Mathf.Lerp(1, 0, linearT);


            yield return null;
        }

        DestroyEffect();


        yield return null;
    }

    private void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
