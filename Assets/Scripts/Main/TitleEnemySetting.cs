using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TitleEnemySetting : MonoBehaviour
{


    [SerializeField]
    private StageHandler stageHandler;

    [SerializeField]
    private TitleEnemyMember[] members;

    private TitleEnemyMember lastSelectedMember;

    [SerializeField]
    private TitlePassiveInfo[] equippedPassives;

    [SerializeField]
    private TitleCardInfo[] equippedCards;

    private CharacterBaseInfo selectedCharacter;

    [SerializeField]
    private TitleCharacterInfo characterInfo;

    [SerializeField]
    private TMP_Text stageName;

    [SerializeField]
    private Image stageLogo;

    [SerializeField]
    private GameObject[] infoObj;

    private void Awake()
    {
        foreach (TitleEnemyMember member in members)
        {
            member.OnCharacterClicked += DisplayMemberInfo;
        }

        stageHandler.OnStageChange += StageChange;

    }

    private void StageChange(Stage stage)
    {
        ChangeStageCharacter(stage);
        ChangeStageTitle(stage);


        if (lastSelectedMember != null) lastSelectedMember.UnSelectUi();

        HideInfoObj();
    }

    private void ChangeStageTitle(Stage stage)
    {

        stageName.text = stage.GetStageName();
        stageLogo.sprite = stage.GetStageLogo();

        stageName.gameObject.SetActive(true);
        stageLogo.gameObject.SetActive(true);

    }

    private void HideInfoObj()
    {
        foreach (GameObject obj in infoObj)
        {
            obj.SetActive(false);
        }
    }

    private void DisplayInfoObj()
    {
        foreach (GameObject obj in infoObj)
        {
            obj.SetActive(true);
        }
    }

    private void RefreshMemberInfo()
    {
        characterInfo.InitInfo(selectedCharacter);
        LoadPassiveInfo();
        LoadCardInfo();
    }

    private void DisplayMemberInfo(TitleEnemyMember member)
    {

        if (lastSelectedMember != null && lastSelectedMember != member) lastSelectedMember.UnSelectUi();

        lastSelectedMember = member;

        selectedCharacter = member.character;

        RefreshMemberInfo();

        DisplayInfoObj();

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

    private void ChangeStageCharacter(Stage stage)
    {
        CharacterBaseInfo[] characters = stage.GetStageCharacters();

        for (int i = 0; i < members.Length; i++)
        {
            if (characters.Length > i)
            {
                members[i].UnlockMember(characters[i]);
            }

            else
            {
                members[i].LockMember();
            }
        }

    }


}
