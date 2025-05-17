using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;
using UnityEngine.Pool;
public class Rencounter : MonoBehaviour
{


    private InBattleAction battleAction;

    public void InitRencounter(InBattleAction battleAction)
    {
        this.battleAction = battleAction;

        cardImg.sprite = battleAction.card.image;

        cardName.text = battleAction.card.cardName;


        resultDice.gameObject.SetActive(false);
        firstDice.gameObject.SetActive(false);
        diceValue.gameObject.SetActive(false);





        
        foreach (InBattleDice inBattleDice in battleAction.inBattleDices)
        {
            RencounterDice rencounterDice = GetDice();

            rencounterDice.InitDice(inBattleDice.dice);

            rencounterDices.Add(rencounterDice);
        }
        





        /*
        
        for (int i = 0; i < nextDices.Count; i++)
        {
            if (battleAction.inBattleDices.Count > i)
            {
                nextDices[i].sprite = ResourceManager.Instance.cardResource.GetDiceSprite(battleAction.inBattleDices[i].dice.diceType);
                nextDices[i].gameObject.SetActive(true);
            }

            else
            {
                nextDices[i].gameObject.SetActive(false);
            }

        }

        */

        gameObject.SetActive(true);
    }

    public void NxtRencounter()
    {
        ResetBattleEffect();

        anim.SetBool("Result", false);





        Dice diceData = rencounterDices[battleAction.currentDiceIndex].diceData;


        firstDice.sprite = ResourceManager.Instance.cardResource.GetDiceSprite(diceData.diceType);
        rencounterDices[battleAction.currentDiceIndex].DestroyDice();

        diceValue.text = string.Format("{0}~{1}", diceData.diceMin, diceData.diceMax);

        if (diceData.diceType is EnumTypes.DiceType.Block or EnumTypes.DiceType.Evade)
        {
            nowDiceHexColor = defenseDiceHexColor;
        }

        else
        {
            nowDiceHexColor = attackDiceHexColor;
        }
        DiceColorChange(nowDiceHexColor);

        resultDice.gameObject.SetActive(false);
        firstDice.gameObject.SetActive(true);
        diceValue.gameObject.SetActive(true);






        /*


        InBattleDice inBattleDice = battleAction.GetCurrentDice();
        Dice dice = inBattleDice.dice;

        if (inBattleDice == null) return;

        firstDice.sprite = nextDices[battleAction.currentDiceIndex].sprite;
        


        resultDice.gameObject.SetActive(false);
        nextDices[battleAction.currentDiceIndex].gameObject.SetActive(false);

        diceValue.text = string.Format("{0}~{1}", dice.diceMin, dice.diceMax);


        if (dice.diceType is EnumTypes.DiceType.Block or EnumTypes.DiceType.Evade)
        {
            nowDiceHexColor = defenseDiceHexColor;
        }

        else
        {
            nowDiceHexColor = attackDiceHexColor;
        }

        DiceColorChange(nowDiceHexColor);


        firstDice.gameObject.SetActive(true);
        diceValue.gameObject.SetActive(true);
        */
    }





    private List<RencounterDice> rencounterDices = new List<RencounterDice>();

    [SerializeField]
    private Transform rencounterDiceTf;

    private IObjectPool<RencounterDice> _Pool;

    [SerializeField]
    private GameObject rencounterDicePrefab;

    public Action onEndCard;



    public void EndCard()
    {
        onEndCard?.Invoke();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        rencounterHandler = GetComponentInParent<RencounterHandler>();
        anim = GetComponent<Animator>();

        _Pool = new ObjectPool<RencounterDice>(CreateDice, OnGetDice, OnReleaseDice, OnDestroyDice, maxSize: 10);
    }



    private RencounterDice CreateDice()
    {
        RencounterDice dice = Instantiate(rencounterDicePrefab).GetComponent<RencounterDice>();

        dice.transform.SetParent(rencounterDiceTf);

        dice.transform.localScale = Vector3.one;

        dice.SetManagedPool(_Pool);

        dice.SetEvent(this);

        return dice;

    }

    private RencounterDice GetDice()
    {
        return _Pool.Get();
    }

    private void OnGetDice(RencounterDice dice)
    {
        dice.gameObject.SetActive(true);
    }

    private void OnReleaseDice(RencounterDice dice)
    {
        dice.gameObject.SetActive(false);
    }

    private void OnDestroyDice(RencounterDice dice)
    {
        Destroy(dice.gameObject);
    }







    












    private CardData cardData;

    [SerializeField]
    private TMP_Text cardName;

    [SerializeField]
    private Image cardImg;

    [SerializeField]
    private Image resultDice;

    [SerializeField]
    private TMP_Text resultValue;

    [SerializeField]
    private Image firstDice;

    [SerializeField]
    private Image loseClashingDice;

    [SerializeField]
    private TMP_Text diceValue;

    [SerializeField]
    private List<Image> nextDices = new List<Image>();

    private RencounterHandler rencounterHandler;

    private Animator anim;

    [SerializeField]
    private Transform battleEffectTransform;

    [SerializeField]
    private string attackDiceHexColor;

    [SerializeField]
    private string defenseDiceHexColor;

    private string nowDiceHexColor;


    private List<BattleEffectAlert> alertList = new List<BattleEffectAlert>();

    
    /*
    private void Awake()
    {
        rencounterHandler = GetComponentInParent<RencounterHandler>();
        anim = GetComponent<Animator>();
        diceHandler = GetComponent<RencounterDiceHandler>();
    }
    */
    
    private void Start()
    {

        gameObject.SetActive(false);


    }
    
    private void InitBattleEffect(BattleEffectAlert alert)
    {
        alertList.Add(alert);
        alert.transform.SetParent(battleEffectTransform);
    }

    private void ResetBattleEffect()
    {
        foreach(BattleEffectAlert alert in alertList)
        {
            alert.DestroyEffect();
        }

        alertList.Clear();
    }


    private void OnEnable()
    {
        rencounterHandler.onNxtRencounter -= NxtRencounter;
        rencounterHandler.onNxtRencounter += NxtRencounter;


        // Evnet sub Check
        rencounterHandler.onNextRencounter -= NextRencounter;
        rencounterHandler.onDiceResult -= DiceResult;
        rencounterHandler.onEndCard -= EndCard;
        rencounterHandler.onLoseClashing -= DiceLoseClashing;
        rencounterHandler.onBattleEffect -= InitBattleEffect;


        rencounterHandler.onNextRencounter += NextRencounter;
        rencounterHandler.onDiceResult += DiceResult;
        rencounterHandler.onEndCard += EndCard;
        rencounterHandler.onLoseClashing += DiceLoseClashing;
        rencounterHandler.onBattleEffect += InitBattleEffect;
    }

    private void OnDisable()
    {
        rencounterHandler.onNxtRencounter -= NxtRencounter;



        rencounterHandler.onNextRencounter -= NextRencounter;
        rencounterHandler.onDiceResult -= DiceResult;
        rencounterHandler.onEndCard -= EndCard;
        rencounterHandler.onLoseClashing -= DiceLoseClashing;
        rencounterHandler.onBattleEffect -= InitBattleEffect;
    }


    private void NextRencounter(int index)
    {
        ResetBattleEffect();

        anim.SetBool("Result", false);

        if (cardData.dice.Length - 1 < index) return;

        firstDice.sprite = nextDices[index].sprite;

        resultDice.gameObject.SetActive(false);
        nextDices[index].gameObject.SetActive(false);

        diceValue.text = string.Format("{0}~{1}", cardData.dice[index].diceMin, cardData.dice[index].diceMax);


        if(cardData.dice[index].diceType is EnumTypes.DiceType.Block or EnumTypes.DiceType.Evade)
        {
            nowDiceHexColor = defenseDiceHexColor;
        }

        else
        {
            nowDiceHexColor = attackDiceHexColor;
        }

        DiceColorChange(nowDiceHexColor);


        firstDice.gameObject.SetActive(true);
        diceValue.gameObject.SetActive(true);
    }

    private void DiceColorChange(string HexCode)
    {
        Color diceColor;
        ColorUtility.TryParseHtmlString(HexCode, out diceColor);

        diceValue.color = diceColor;
        resultDice.color = diceColor;
        loseClashingDice.color = diceColor;
    }


    public void DiceLoseClashing()
    {
        resultDice.gameObject.SetActive(false);

        diceValue.gameObject.SetActive(false);

        anim.SetTrigger("Lose");

    }


    public void DiceResult(int value)
    {

        
        StringBuilder valueSb = new StringBuilder();

        string valueString = value.ToString();

        for (int i = 0; i < valueString.Length; i++)
        {
            valueSb.Append("<sprite=" + valueString[i] + ", color=" + nowDiceHexColor + ">");
        }

        resultValue.text = valueSb.ToString();


        firstDice.gameObject.SetActive(false);
        resultDice.gameObject.SetActive(true);

        anim.SetBool("Result", true);
    }


    public void SetRencounter(CardData card)
    {   
        cardData = card;

        cardImg.sprite = card.image;

        cardName.text = card.cardName;

        resultDice.gameObject.SetActive(false);
        firstDice.gameObject.SetActive(false);
        diceValue.gameObject.SetActive(false);


        for (int i = 0; i < nextDices.Count; i++)
        {
            if(card.dice.Length > i)
            {
                nextDices[i].sprite = ResourceManager.Instance.cardResource.GetDiceSprite(card.dice[i].diceType);
                nextDices[i].gameObject.SetActive(true);
            }

            else
            {
                nextDices[i].gameObject.SetActive(false);
            }
           
        }

        gameObject.SetActive(true);
    }


}
