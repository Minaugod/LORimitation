using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CostLight : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Image enableLight;

    public void BlinkLight()
    {
        anim.SetBool("Blink", true);
    }

    public void ResetBlink()
    {
        anim.SetBool("Blink", false);
    }

    public void EnableLight()
    {
        enableLight.gameObject.SetActive(true);
    }

    public void DisableLight()
    {
        enableLight.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        Destroy(gameObject);
    }


}
