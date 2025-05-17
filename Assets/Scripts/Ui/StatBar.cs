using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatBar : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private Image staggerBar;

    public Transform buffTransform;

    [SerializeField]
    private TMPro.TMP_Text hpText;

    [SerializeField]
    private TMPro.TMP_Text staggerText;


    int maxHp, maxStagger;

    int nowHp, nowStagger;

    private Character character;

    [SerializeField]
    private Vector3 offset;

    public void ChangeToBattleCanvas()
    {
        UiManager.Instance.ChangeToBattleCanvas(transform);
    }

    public void ChangeToDefaultCanvas()
    {
        UiManager.Instance.ChangeToStatBarCanvas(transform);
    }

    public void Init(Character character)
    {
        this.character = character;

        if (character.IsEnemy)
        {
            offset.x = -0.06f;
        }

        maxHp = character.stat.Hp;
        maxStagger = character.stat.Stagger;

        nowHp = maxHp;
        nowStagger = maxStagger;

        hpText.text = nowHp.ToString();
        staggerText.text = nowStagger.ToString();

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (character != null)
        {
            character.onBattleLayer += ChangeToBattleCanvas;
            character.onDefaultLayer += ChangeToDefaultCanvas;

            character.stat.onHpChanged += HpChanged;
            character.stat.onStaggerChanged += StaggerChanged;
            character.stat.onAddBuff += AddBuff;
            character.stat.onDeadCharacter += DeadCharacter;
        }

        if (BattleManager.Instance != null)
        {
            BattleManager.Instance.onEndAct += HideStatBar;
            BattleManager.Instance.onNextAct += DisplayStatBar;
            BattleManager.Instance.onEndGame += DeadCharacter;
        }
    }

    private void UnsubscribeEvents()
    {
        if (character != null)
        {
            character.onBattleLayer -= ChangeToBattleCanvas;
            character.onDefaultLayer -= ChangeToDefaultCanvas;

            character.stat.onHpChanged -= HpChanged;
            character.stat.onStaggerChanged -= StaggerChanged;
            character.stat.onAddBuff -= AddBuff;
            character.stat.onDeadCharacter -= DeadCharacter;
        }

        if (BattleManager.Instance != null)
        {
            BattleManager.Instance.onEndAct -= HideStatBar;
            BattleManager.Instance.onNextAct -= DisplayStatBar;
            BattleManager.Instance.onEndGame -= DeadCharacter;
        }
    }


    private void DeadCharacter()
    {
        UnsubscribeEvents();
        Destroy(gameObject);

    }

    private void AddBuff(BuffUi buff)
    {
        buff.transform.SetParent(buffTransform);
        buff.transform.localPosition = Vector3.zero;
    }

    public void HideStatBar()
    {
        gameObject.SetActive(false);
    }

    public void DisplayStatBar()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        character.stat.onHpChanged -= HpChanged;
        character.stat.onStaggerChanged -= StaggerChanged;
    }

    private void OnEnable()
    {
        if(character != null)
        {
            character.stat.onHpChanged -= HpChanged;
            character.stat.onStaggerChanged -= StaggerChanged;

            character.stat.onHpChanged += HpChanged;
            character.stat.onStaggerChanged += StaggerChanged;

            HpChanged();
            StaggerChanged();

        }

    }

    private void HpChanged()
    {
        int previousValue = nowHp;
        int changedValue = character.stat.Hp;

        nowHp = changedValue;

        hpText.text = changedValue.ToString();


        StartCoroutine(BarChangeCoroutine(hpBar, previousValue, changedValue));
    }

    private void StaggerChanged()
    {
        int previousValue = nowStagger;
        int changedValue = character.stat.Stagger;

        nowStagger = changedValue;

        staggerText.text = changedValue.ToString();


        StartCoroutine(BarChangeCoroutine(staggerBar, previousValue, changedValue));

    }

    IEnumerator BarChangeCoroutine(Image bar, float previousValue, float changedValue)
    {

        float previousAmount = previousValue / maxHp;
        float changedAmount = changedValue / maxHp;


        float time = 0f;
        float duration = 0.3f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            bar.fillAmount = Mathf.Lerp(previousAmount, changedAmount, linearT);

            yield return null;
        }

        yield return null;
    }

    private void Update()
    {
        if(character != null)
        {
            transform.position = character.transform.position + offset;

        }

    }
}
