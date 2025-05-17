using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameEffect : MonoBehaviour
{
    [SerializeField]
    private Animator victoryAnim;

    [SerializeField]
    private Animator defeatAnim;

    [SerializeField]
    private GameObject victoryObj;

    [SerializeField]
    private GameObject defeatObj;

    public void DisableEffect()
    {
        victoryObj.SetActive(false);
        defeatObj.SetActive(false);
    }

    public void DisplayEffect(EnumTypes.GameResult result)
    {

        switch (result)
        {
            case EnumTypes.GameResult.Victory:
                victoryObj.SetActive(true);
                victoryAnim.SetBool("Change", true);
                break;

            case EnumTypes.GameResult.Defeat:

                defeatObj.SetActive(true);
                defeatAnim.SetBool("Change", true);
                break;
        }


    }


}
