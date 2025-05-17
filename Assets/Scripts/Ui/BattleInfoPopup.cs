using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoPopup : MonoBehaviour
{

    [SerializeField]
    private TMPro.TMP_Text infoText;

    public void DisplayPopup(string text, Vector3 pos)
    {
        infoText.text = text;

        transform.position = pos;

        gameObject.SetActive(true);
        
    }

    public void DisablePopup()
    {
        gameObject.SetActive(false);
    }


}
