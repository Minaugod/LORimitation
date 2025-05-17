using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUp : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text value;

    [SerializeField]
    private float speed;

    public void Init(Transform target, int value)
    {
        transform.position = target.position;
        this.value.text = value.ToString();
    }

    private void OnEnable()
    {
        Invoke("DestroyEffect", 2f);


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
