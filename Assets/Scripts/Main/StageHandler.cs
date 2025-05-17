using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class StageHandler : MonoBehaviour
{

    public event Action<Stage> OnStageChange;

    [SerializeField]
    private TitleMember[] members;

    private Stage lastStage;

    [SerializeField]
    private Button startBtn;

    [SerializeField]
    private Stage[] stages;


    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TMP_Text activeMemberCountText;

    private int activeMemberCount = 0;

    private void Awake()
    {

        foreach (TitleMember member in members)
        {
            member.OnToggleActive += ToggleActiveMember;
        }

        foreach (Stage stage in stages)
        {
            stage.OnStageClicked += StageChange;
        }

        RefreshStartButton();
    }


    private void ToggleActiveMember(bool isActive)
    {

        if (isActive) activeMemberCount++;
        else activeMemberCount--;

        RefreshStartButton();
    }

    private void RefreshStartButton()
    {
        activeMemberCountText.text = string.Format("선택된 인원 : {0}/3", activeMemberCount);

        if (lastStage != null && activeMemberCount > 0)
        {
            animator.SetBool("Active", true);
        }

        else
        {
            animator.SetBool("Active", false);
        }
    }



    private void StageChange(Stage stage)
    {


        if (lastStage != null) lastStage.UnSelect();

        lastStage = stage;

        OnStageChange.Invoke(lastStage);

        RefreshStartButton();


    }

    public void StartGame()
    {
        Invoke("ChangeScene", 1f);
        UiManager.Instance.gameStateEffect.ScreenFadeOut();



        CharacterBaseInfo[] stageCharacter = lastStage.GetStageCharacters();

        ResourceManager.Instance.SettingEnemyCharacter(stageCharacter);

        ResourceManager.Instance.SettingTeamCharacter(members);


    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("BattleScene");
    }




}
