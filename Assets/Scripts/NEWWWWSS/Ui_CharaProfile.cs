using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class Ui_CharaProfile : MonoBehaviour
{
    public Librarian librarian { get; private set; }

    [SerializeField]
    private TMP_Text nameText;

    public RawImage portraitImage;

    [SerializeField]
    private GameObject unselectedProfile;

    [SerializeField]
    private GameObject selectedProfile;

    [SerializeField]
    private Vector3 selectUiScale;

    public UnityEvent<Ui_CharaProfile> onSelectProfile;

    bool isSelected = false;

    public void SetCharaProfile(Librarian librarian)
    {
        this.librarian = librarian;

        nameText.text = librarian.name;

        portraitImage.texture = librarian.portraitTexture;
    }


    public void SelectProfile()
    {
        if (!isSelected)
        {
            isSelected = true;

            transform.localScale = selectUiScale;

            unselectedProfile.SetActive(false);

            selectedProfile.SetActive(true);


            onSelectProfile?.Invoke(this);
            //taskPanel.SelectCharaProfile(this);
            //charaDetail.SetInfo(this);

        }

    }

    public void DeSelectProfile()
    {
        isSelected = false;

        transform.localScale = Vector3.one;

        unselectedProfile.SetActive(true);

        selectedProfile.SetActive(false);

    }
    

}
