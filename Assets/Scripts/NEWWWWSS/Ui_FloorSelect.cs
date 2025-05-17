using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_FloorSelect : MonoBehaviour
{

    public Animator anim;

    public int floorIndex;

    [SerializeField]
    private Ui_RightSideBar rightSideBar;
    public void SelectFloor()
    {
        rightSideBar.ShowFloorInfo(floorIndex);
    }


    
    public void OnHover()
    {
        anim.SetBool("IsHover", true);
    }

    public void ExitHover()
    {
        anim.SetBool("IsHover", false);
    }

}
