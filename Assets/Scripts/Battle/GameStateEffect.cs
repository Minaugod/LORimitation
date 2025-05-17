using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateEffect : MonoBehaviour
{
    [SerializeField]
    private Animator actChangeAnim;

    [SerializeField]
    private TMPro.TMP_Text actText;

    [SerializeField]
    private Animator victoryAnim;

    [SerializeField]
    private Animator defeatAnim;

    [SerializeField]
    private GameObject victoryObj;

    [SerializeField]
    private GameObject defeatObj;

    [SerializeField]
    private Animator screenFadeAnim;

    public void ScreenFadeOut()
    {
        screenFadeAnim.SetBool("Fade", true);
    }

    public void ScreenFadeIn()
    {
        screenFadeAnim.SetBool("Fade", false);
    }

    public void ActChange(int value)
    {
        actText.text = string.Format("Á¦ {0}¸·", value);
        actChangeAnim.SetTrigger("Change");

    }

    public void DisplayResult(EnumTypes.GameResult result)
    {

        switch (result)
        {
            case EnumTypes.GameResult.Victory:
                
                victoryAnim.SetBool("Display", true);
                break;

            case EnumTypes.GameResult.Defeat:
                defeatAnim.SetBool("Display", true);
                break;
        }


    }

    public void DisableResult()
    {
        victoryAnim.SetBool("Display", false);
        defeatAnim.SetBool("Display", false);
    }




}
