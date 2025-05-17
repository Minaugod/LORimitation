using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosingTargetArrow : MonoBehaviour
{


    public void DisplayArrow()
    {
        transform.position = Input.mousePosition;
        gameObject.SetActive(true);
    }

    public void DisableArrow()
    {
        gameObject.SetActive(false);
    }


    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
