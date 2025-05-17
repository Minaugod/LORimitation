using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActChangeEffect : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject effectObj;

    [SerializeField]
    private TMPro.TMP_Text act;

    public void ActChange(int value)
    {
        effectObj.SetActive(true);
        act.text = string.Format("Á¦ {0}¸·", value);
        anim.SetBool("Change", true);

    }

    public void EndAnim()
    {
        effectObj.SetActive(false);
        anim.SetBool("Change", false);
    }


}
