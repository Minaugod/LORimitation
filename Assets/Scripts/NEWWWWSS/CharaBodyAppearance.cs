using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBodyAppearance : MonoBehaviour
{

    public EnumTypes.CharacterState bodyState;

    public List<GameObject> defaultHeadparts;

    public Transform headTf;

    public void SetHeadToBody(GameObject head)
    {
        head.transform.SetParent(transform);

        head.transform.localPosition = headTf.localPosition;


        head.SetActive(true);

        HideDefaultHeadParts();
    }


    public void HideDefaultHeadParts()
    {
        foreach (GameObject parts in defaultHeadparts)
        {
            parts.SetActive(false);
        }
    }

}
