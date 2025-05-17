using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCustomizeSetter : MonoBehaviour
{

    public List<CustomizeHead> headList;

    public HeadCustomizeData headCustomizeData;

    

    public void SetCustomize(Librarian librarian)
    {
        if(librarian is MemberLibrarian)
        {
            MemberLibrarian member = (MemberLibrarian)librarian;

            foreach (var head in headList)
            {
                head.SetCustomizeData(member.customizeData);
            }
        }

        else
        {
            return;
        }


    }

    public GameObject GetHead(EnumTypes.CharacterState state)
    {


        foreach(var head in headList)
        {
            if (head.headState == state) 
            { 
                return head.gameObject;
            }
        }

        return null;

        

    }  


}
