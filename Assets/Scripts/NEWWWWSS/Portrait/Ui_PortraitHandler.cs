using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_PortraitHandler : MonoBehaviour
{
    [SerializeField]
    private List<Ui_PortraitSetter> portraitSetters;


    private void Start()
    {
        StartCoroutine(WaitDataLoad());
        //ShowFloorInfo(0);
    }

    IEnumerator WaitDataLoad()
    {
        yield return new WaitUntil(() => DataManager.Inst.IsDataLoaded());

        SetPortraits();


    }
    public void SetPortraits()
    {

        List<Librarian> librarians = new List<Librarian>();

        foreach (var floor in DataManager.Inst.librariansFloors)
        {
            foreach (var librarian in floor.GetLibrarians())
            {
                librarians.Add(librarian);
            }
        }


        for (int i = 0; i < librarians.Count; ++i)
        {
            portraitSetters[i].InitLibrarian(librarians[i]);
        }


    }

}
