using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EmotionLevel : MonoBehaviour
{

    private Character character;


    [SerializeField]
    private TMPro.TMP_Text characterName;

    [SerializeField]
    private RawImage characterPortrait;

    [SerializeField]
    private TMPro.TMP_Text emotionLvText;

    [SerializeField]
    private TMPro.TMP_Text hpValue;

    private int nowHp;

    [SerializeField]
    private Slider hpBar;

    private int emotionLv;

    private int currentEmotionCoin;

    private int emotionCoinMax;

    private EmotionCoinSlot[] coinSlot;


    private void Awake()
    {
        coinSlot = GetComponentsInChildren<EmotionCoinSlot>();
    }
    private void OnEnable()
    {
        ResetProfile();
    }
    private void ResetProfile()
    {
        if(character == null)
        {
            hpValue.text = null;
            emotionLvText.text = null;
            characterName.text = null;
            hpBar.value = 0;

            gameObject.SetActive(false);
        }

    }


    public void Init(Character character)
    {

        emotionLv = 0;

        characterName.text = character.stat.characterName;

        characterPortrait.texture = character.renderTexture;

        nowHp = character.stat.Hp;
        hpBar.maxValue = nowHp;
        hpBar.value = nowHp;
        hpValue.text = nowHp.ToString();

        SetEmotionLevelData(character);

        BattleManager.Instance.onEndAct += CheckEmotionLevelUp;


        character.stat.onHpChanged += HpChanged;

        character.behaviour.onWinClash += WinClash;
        character.behaviour.onLoseClash += LoseClash;
        character.behaviour.onMaxValue += GetPositiveCoin;
        character.behaviour.onMinValue += GetNegativeCoin;

        this.character = character;

        gameObject.SetActive(true);
    }

    private void HpChanged()
    {
        StartCoroutine(HpChangeCoroutine());

    }

    IEnumerator HpChangeCoroutine()
    {
        int previousValue = nowHp;
        int changedValue = character.stat.Hp;

        hpValue.text = changedValue.ToString();
        nowHp = changedValue;

        float time = 0f;
        float duration = 0.3f;

        while(time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            hpBar.value = Mathf.Lerp(previousValue, changedValue, linearT);

            yield return null;
        }

        yield return null;
    }



    private void SetEmotionLevelData(Character character)
    {
        EmotionLevelEffect emotionLvEffect = ResourceManager.Instance.emotionLevelEffects[emotionLv];

        emotionLvEffect.ApplyEffect(character);

        currentEmotionCoin = 0;

        emotionCoinMax = emotionLvEffect.requireEmotionCoin;

        emotionLvText.text = emotionLvEffect.effectLv;

        for (int i = 0; i < coinSlot.Length; i++)
        {
            if(i < emotionCoinMax)
            {
                coinSlot[i].ResetSlot();
            }

            else
            {
                coinSlot[i].gameObject.SetActive(false); ;
            }
        }

    }

    private void EmotionLevelUp()
    {
        emotionLv++;

        SetEmotionLevelData(character);
    }

    private void WinClash(Character winner, Character loser)
    {
        GetPositiveCoin();
    }

    private void LoseClash(Character loser)
    {
        GetNegativeCoin();
    }

    private void GetPositiveCoin()
    {


        if(currentEmotionCoin >= emotionCoinMax || emotionLv >= ResourceManager.Instance.emotionLevelEffects.Length - 1)
        {
            return;
        }

        coinSlot[currentEmotionCoin].GetPositiveCoin(character);

        currentEmotionCoin++;
    }

    private void GetNegativeCoin()
    {
        if (currentEmotionCoin >= emotionCoinMax || emotionLv >= ResourceManager.Instance.emotionLevelEffects.Length - 1)
        {
            return;
        }

        coinSlot[currentEmotionCoin].GetNegativeCoin(character);

        currentEmotionCoin++;
    }

    private void CheckEmotionLevelUp()
    {

        if (emotionLv < ResourceManager.Instance.emotionLevelEffects.Length - 1)
        {
            if (currentEmotionCoin >= emotionCoinMax)
            {
                EmotionLevelUp();
            }
        }



    }

}