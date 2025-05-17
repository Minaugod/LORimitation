using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    public Camera uiCamera;

    [Header("Ui")]
    public GameStateEffect gameStateEffect;

    public CardHandler cardHandler;

    public TeamInfoPanel enemyInfoPanel;

    public TeamInfoPanel teamInfoPanel;

    public BattleInfoPopup infoPopup;

    public EmotionLevel[] teamEmotions;

    public EmotionLevel[] enemyEmotions;



    [Header("Canvas")]
    public Transform fixedCanvas;

    public Transform rencounterCanvas;

    public Transform statBarCanvas;

    public Transform battleCanvas;

    public Transform battleUiCanvas;

    public Transform battleProfileCanvas;

    [Header("Prefab")]
    public GameObject healPrefab;

    public GameObject attackDmgLogPrefab;

    public GameObject attackStaggerLogPrefab;

    public GameObject dmgLogPrefab;

    public GameObject staggerLogPrefab;

    public GameObject onMaximumDamage;

    public GameObject onStaggerdPrefab;

    public GameObject statBarPrefab;

    public GameObject rencounterPrefab;

    public GameObject diceHandlerPrefab;

    public GameObject teamDicePrefab;

    public GameObject enemyDicePrefab;

    public GameObject lightPrefab;

    public GameObject targetArrowPrefab;


    public GameObject counterPreviewPrefab;

    public DiceHandler EquipDiceForCharacter(Character character)
    {
        DiceHandler diceHandler = Instantiate(diceHandlerPrefab).GetComponent<DiceHandler>();

        diceHandler.transform.SetParent(fixedCanvas);

        diceHandler.transform.position = character.transform.position + new Vector3(0f, 1.5f, 0f);

        diceHandler.Init(character);

        return diceHandler;

    }

    public void HideInfo()
    {
        cardHandler.HideCard();
        enemyInfoPanel.HideInfo();
        teamInfoPanel.HideInfo();
    }

    public void ChangeToBattleCanvas(Transform transform)
    {
        transform.SetParent(battleCanvas);
    }

    public void ChangeToStatBarCanvas(Transform transform)
    {
        transform.SetParent(statBarCanvas);
    }



    public void AddEmotionLevel(Character character)
    {
        if (character.IsEnemy) enemyEmotions[character.id].Init(character);

        else teamEmotions[character.id].Init(character);
    }



    public void AddStatBar(Character character)
    {

        StatBar statBar = Instantiate(statBarPrefab).GetComponent<StatBar>();
        statBar.transform.SetParent(statBarCanvas);
        statBar.Init(character);
    }

    public RencounterHandler AddRencounter(Character character)
    {
        RencounterHandler rencounter = Instantiate(rencounterPrefab).GetComponent<RencounterHandler>();
        rencounter.transform.SetParent(rencounterCanvas);
        rencounter.InitTarget(character);

        return rencounter;
    }

    public CounterDicePreview AddCounterDicePreview(Character character)
    {
        CounterDicePreview counterDicePreview = Instantiate(counterPreviewPrefab).GetComponent<CounterDicePreview>();

        counterDicePreview.transform.SetParent(statBarCanvas);
        counterDicePreview.InitCharacter(character);

        return counterDicePreview;

    }

    public DiceController AddDice(bool isEnemy)
    {

        GameObject dice;

        if (isEnemy) dice = Instantiate(enemyDicePrefab);
        else dice = Instantiate(teamDicePrefab);

        return dice.GetComponent<DiceController>();
    }

    public TargetArrow AddTargetArrow()
    {

        GameObject arrow = Instantiate(targetArrowPrefab);

        return arrow.GetComponent<TargetArrow>();
    }

    public void DisplayHeal(Transform target, int value)
    {
        StatusUp hp = Instantiate(healPrefab).GetComponent<StatusUp>();
        hp.transform.SetParent(fixedCanvas);
        hp.Init(target, value);
    }

    
    public void DisplayAttackDmg(Character target, EnumTypes.DiceType type, int value)
    {
        AttackDmgEffectBase dmg = Instantiate(attackDmgLogPrefab).GetComponent<AttackDmgEffectBase>();
        dmg.transform.SetParent(fixedCanvas);
        dmg.Init(target, type, value);

    }

    public void DisplayAttackStagger(Character target, EnumTypes.DiceType type, int value)
    {
        AttackDmgEffectBase dmg = Instantiate(attackStaggerLogPrefab).GetComponent<AttackDmgEffectBase>();
        dmg.transform.SetParent(fixedCanvas);
        dmg.Init(target, type, value);

    }

    public void DisplayDmg(Transform target, int value)
    {
        DmgEffect dmg = Instantiate(dmgLogPrefab).GetComponent<DmgEffect>();
        dmg.transform.SetParent(fixedCanvas);
        dmg.Init(target, value);
    }


    public void DisplayStagger(Transform target, int value)
    {
        DmgEffect dmg = Instantiate(staggerLogPrefab).GetComponent<DmgEffect>();
        dmg.transform.SetParent(fixedCanvas);
        dmg.Init(target, value);
    }

    public void DisplayOnStaggered(Transform target)
    {

        OnBattleEffect effect = Instantiate(onStaggerdPrefab).GetComponent<OnBattleEffect>();

        effect.transform.SetParent(fixedCanvas);
        effect.Init(target);

    }

    public void DisplayOnMaximumDamage(Transform target)
    {

        OnBattleEffect effect = Instantiate(onMaximumDamage).GetComponent<OnBattleEffect>();

        effect.transform.SetParent(fixedCanvas);
        effect.Init(target);

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameStateEffect.DisableResult();
        gameStateEffect.ScreenFadeIn();

        if (scene.name == "MainScene")
        {
            battleProfileCanvas.gameObject.SetActive(false);
            battleUiCanvas.gameObject.SetActive(false);
        }

        if (scene.name == "BattleScene")
        {
            battleProfileCanvas.gameObject.SetActive(true);
            battleUiCanvas.gameObject.SetActive(true);
        }

    }

    public void CardInit(Character character)
    {
        cardHandler.CardInit(character);
    }





    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    private static UiManager instance;

    public static UiManager Instance
    {
        get
        { 
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }

    }

}
