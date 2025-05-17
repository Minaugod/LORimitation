using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TestEvee : MonoBehaviour
{

    public UnityEvent evente;

    public TestEvee ev2;
    // Start is called before the first frame update
    void Start()
    {
        evente.AddListener(ev2.Acc);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EventOn()
    {
        evente.Invoke();
    }

    public void Acc()
    {
        print("ASD");
    }

}
