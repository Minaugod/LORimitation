using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TitleCharacterInfo : MonoBehaviour
{

    [SerializeField]
    private TMP_Text hpText;

    [SerializeField]
    private TMP_Text staggerText;

    [SerializeField]
    private TMP_Text speedText;

    [SerializeField]
    private Image thumb;

    [SerializeField]
    private TMP_Text slashResist;

    [SerializeField]
    private TMP_Text pierceResist;

    [SerializeField]
    private TMP_Text bluntResist;

    [SerializeField]
    private TMP_Text slashStaggerResist;

    [SerializeField]
    private TMP_Text pierceStaggerResist;

    [SerializeField]
    private TMP_Text bluntStaggerResist;

    public void InitInfo(CharacterBaseInfo character)
    {

        hpText.text = character.keyPage.page.hp.ToString();

        staggerText.text = character.keyPage.page.staggerResist.ToString();

        speedText.text = string.Format("{0}~{1}", character.keyPage.page.spdDiceMin, character.keyPage.page.spdDiceMax);

        thumb.sprite = character.thumb; 

        slashResist.text = ResourceManager.Instance.resistResource.FindResistText(character.keyPage.page.slashDmgResist);

        pierceResist.text = ResourceManager.Instance.resistResource.FindResistText(character.keyPage.page.pierceDmgResist);

        bluntResist.text = ResourceManager.Instance.resistResource.FindResistText(character.keyPage.page.bluntDmgResist);

        slashStaggerResist.text = ResourceManager.Instance.resistResource.FindResistText(character.keyPage.page.slashStaggerResist);

        pierceStaggerResist.text = ResourceManager.Instance.resistResource.FindResistText(character.keyPage.page.pierceStaggerResist);

        bluntStaggerResist.text = ResourceManager.Instance.resistResource.FindResistText(character.keyPage.page.bluntStaggerResist);

    }



}
