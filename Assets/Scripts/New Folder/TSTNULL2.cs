using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSTNULL2 : MonoBehaviour
{
    public NULLLLLE nulle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(m());
    }

    IEnumerator m()
    {
        yield return new WaitForSeconds(5f);

        nulle.ali = null;
    }
}
