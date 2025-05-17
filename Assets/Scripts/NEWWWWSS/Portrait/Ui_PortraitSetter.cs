using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_PortraitSetter : MonoBehaviour
{
    Librarian portraitLibrarian;

    CharaPortraitSetter currentPortrait;

    [SerializeField]
    private RenderTexture portraitTexture;

    public void InitLibrarian(Librarian librarian)
    {
        portraitLibrarian = librarian;


        SetCharacterAppearance();

        librarian.portraitTexture = portraitTexture;

        librarian.onKeyPageChanged += SetCharacterAppearance;
    }

    public void SetCharacterAppearance()
    {

        if (currentPortrait != null) Destroy(currentPortrait.gameObject);

        KeyPage keyPage = portraitLibrarian.keyPage;
        GameObject portrait = Instantiate(keyPage.page.appearance, transform);

        currentPortrait = portrait.GetComponent<CharaPortraitSetter>();

        currentPortrait.transform.localPosition = Vector3.zero;

        currentPortrait.LibrarianPortraitSet(portraitLibrarian);




    }
}
