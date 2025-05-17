using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgEffect : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text valueText;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float speed;

    private void OnEnable()
    {
        Invoke("DestroyEffect", 1f);

    }

    public void Init(Transform target, int value)
    {
        transform.position = target.position + offset;
        valueText.text = value.ToString();

    }

    private void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
    }


    private void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
