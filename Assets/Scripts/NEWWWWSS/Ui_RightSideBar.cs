using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_RightSideBar : MonoBehaviour
{

    public List<Ui_CharaProfile> charaProfiles;

    private void Start()
    {
        StartCoroutine(ShowFloorInfoCor(0));
    }

    IEnumerator ShowFloorInfoCor(int floorIndex)
    {
        yield return new WaitUntil(()=> DataManager.Inst.IsDataLoaded());

        ShowFloorInfo(floorIndex);


    }
    public void ShowFloorInfo(int floorIndex)
    {

        List<Librarian> librarians = DataManager.Inst.librariansFloors[floorIndex].GetLibrarians();

        for (int i = 0; i < librarians.Count; ++i)
        {

            charaProfiles[i].SetCharaProfile(librarians[i]);

        }


    }




}
