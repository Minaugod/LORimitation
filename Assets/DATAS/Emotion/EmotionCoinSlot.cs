using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EmotionCoinSlot : MonoBehaviour
{
    [SerializeField]
    private Image emotionCoinImage;

    [SerializeField]
    private Color positiveColor;

    [SerializeField]
    private Color negativeColor;


    public void GetPositiveCoin(Character character)
    {
        EmotionTrail trail = EmotionPool.Instance.GetTrail();

        trail.GetPositiveCoin(character.ui.Rencounter.transform.position, transform.position);
        Invoke("SetPositiveColor", 0.5f);
    }

    public void GetNegativeCoin(Character character)
    {
        EmotionTrail trail = EmotionPool.Instance.GetTrail();
        trail.GetNegativeCoin(character.ui.Rencounter.transform.position, transform.position);
        Invoke("SetNegativeColor", 0.5f);

    }


    private void SetPositiveColor()
    {
        emotionCoinImage.color = positiveColor;
        emotionCoinImage.canvasRenderer.SetAlpha(1f);
    }

    private void SetNegativeColor()
    {
        emotionCoinImage.color = negativeColor;
        emotionCoinImage.canvasRenderer.SetAlpha(1f);
    }


    public void ResetSlot()
    {
        gameObject.SetActive(true);
        emotionCoinImage.color = Color.white;
        emotionCoinImage.canvasRenderer.SetAlpha(0.2f);
    }
}
 