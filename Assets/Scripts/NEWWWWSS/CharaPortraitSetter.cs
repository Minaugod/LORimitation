using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaPortraitSetter : MonoBehaviour
{


    /*
     
    0 = Def
    1 = Mov
    2 = Damage
    3 = Evade
    4 = Guard
    5 = Slash
    6 = Pene
    7 = Hit

    */

    public CharaBodyAppearance[] bodyAppearances;


    public void LibrarianPortraitSet(Librarian librarian)
    {

        GameObject headPrefab = librarian.characterHead;


        HeadCustomizeSetter customizeHead = Instantiate(headPrefab).GetComponent<HeadCustomizeSetter>();


        customizeHead.SetCustomize(librarian);


        foreach(var body in bodyAppearances)
        {
            GameObject head = customizeHead.GetHead(body.bodyState);


            body.SetHeadToBody(head.gameObject);


        }

        Destroy(customizeHead.gameObject);


    }



}
