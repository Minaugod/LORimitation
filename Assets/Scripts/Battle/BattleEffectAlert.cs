using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Pool;

public class BattleEffectAlert : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TMP_Text desc;

    private CanvasGroup canvasGroup;

    private IObjectPool<BattleEffectAlert> _ManagedPool;
    public void SetManagedPool(IObjectPool<BattleEffectAlert> pool)
    {
        _ManagedPool = pool;
    }

    public void DestroyEffect()
    {
        StartCoroutine(FadeOutAndRelease());
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    [SerializeField]
    private Sprite passiveIcon;

    [SerializeField]
    private Sprite diceIcon;


    public void InitDiceEffect(Character character, DiceEffect effect)
    {
        icon.sprite = diceIcon;

        desc.text = effect.desc;

        DisplayAlert(character);
    }

    public void InitPassiveEffect(Character character, PassiveEffect effect)
    {
        icon.sprite = passiveIcon;

        desc.text = effect.passiveDesc;

        DisplayAlert(character);
    }

    public void InitBuffEffect(Character character, Sprite icon, string desc)
    {

        this.icon.sprite = icon;

        this.desc.text = desc;

        DisplayAlert(character);
    }

    private void DisplayAlert(Character character)
    {
        character.ui.Rencounter.BattleEffect(this);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0f;

        float time = 0f;
        float duration = 0.3f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            canvasGroup.alpha = Mathf.Lerp(0, 1f, linearT);

            yield return null;
        }

        yield return null;
    }
    IEnumerator FadeOutAndRelease()
    {
        canvasGroup.alpha = 1f;

        float time = 0f;
        float duration = 0.5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            canvasGroup.alpha = Mathf.Lerp(1f, 0, linearT);

            yield return null;
        }

        _ManagedPool.Release(this);
        

        yield return null;
    }
}
