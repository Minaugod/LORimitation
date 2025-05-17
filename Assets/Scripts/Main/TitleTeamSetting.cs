using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTeamSetting : MonoBehaviour
{

    [SerializeField]
    private TitleMember[] members;

    private TitleMember lastSelectedMember;


    [SerializeField]
    private TitlePassiveInfo[] equippedPassives;

    [SerializeField]
    private TitleCardInfo[] equippedCards;

    private CharacterBaseInfo selectedCharacter;

    [SerializeField]
    private TitleCharacterInfo characterInfo;

    [SerializeField]
    private TitlePassiveSetting passiveSetting;

    [SerializeField]
    private TitleCardSetting cardSetting;


    [SerializeField]
    private GameObject[] infoObj;

    private void Awake()
    {

        foreach(TitleMember member in members)
        {
            member.OnCharacterClicked += DisplayMemberInfo;
        }

        passiveSetting.OnSaveSetting += RefreshMemberInfo;

        cardSetting.OnSaveSetting += RefreshMemberInfo;

    }

    private void DisplayInfo()
    {
        foreach(GameObject obj in infoObj)
        {
            obj.SetActive(true);
        }
    }

    private void RefreshMemberInfo()
    {
        LoadPassiveInfo();
        LoadCardInfo();
    }

    private void DisplayMemberInfo(TitleMember member)
    {

        if (lastSelectedMember != null) lastSelectedMember.UnSelectUi();

        lastSelectedMember = member;

        selectedCharacter = member.character;

        characterInfo.InitInfo(selectedCharacter);

        RefreshMemberInfo();

        DisplayInfo();

    }

    private void LoadPassiveInfo()
    {
        // info
        for (int i = 0; i < equippedPassives.Length; i++)
        {
            if (i < selectedCharacter.keyPage.passiveEffects.Count)
            {
                equippedPassives[i].EquipPassive(selectedCharacter.keyPage.passiveEffects[i]);
            }
            else
            {
                equippedPassives[i].UnEquip();
            }
        }
    }

    private void LoadCardInfo()
    {

        for (int i = 0; i < equippedCards.Length; i++)
        {

            equippedCards[i].EquipCard(selectedCharacter.keyPage.cards[i]);
        }

    }

    public void OpenPassiveSetting()
    {
        if(selectedCharacter != null)
        {
            passiveSetting.DisplayPassiveInfo(selectedCharacter);
        }

    }

    public void OpenCardSetting()
    {
        if (selectedCharacter != null)
        {
            cardSetting.DisplayCardInfo(selectedCharacter);
        }

    }

}
