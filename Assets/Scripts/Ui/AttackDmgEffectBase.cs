using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EnumTypes;
using System.Text;
public abstract class AttackDmgEffectBase : MonoBehaviour
{


    [SerializeField]
    protected Image resistImg;

    [SerializeField]
    protected TMP_Text resistText;

    [SerializeField]
    protected TMP_Text valueText;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float downHeight;

    private Transform target;

    private void OnEnable()
    {
        Invoke("DestroyEffect", 0.4f);
    }



    private void DestroyEffect()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        float duration = 0.7f;
        float time = 0.0f;

        float height = 0.5f;
        float width = 0.4f;

        if(transform.position.x - target.position.x < 0) { width *= -1; }

        resistImg.CrossFadeAlpha(0, 0.5f, false);
        resistText.CrossFadeAlpha(0, 0.5f, false);
        valueText.CrossFadeAlpha(0, 0.5f, false);

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            transform.position = target.position + offset + new Vector3(Mathf.Lerp(0, width, linearT), Mathf.Lerp(0, height, linearT));

            yield return null;
        }

        Destroy(gameObject);
    }



    public abstract Resist CalculateResist(Character character, DiceType type);

    public void Init(Character character, DiceType type, int value)
    {
        target = character.transform;

        if (character.IsEnemy) offset.x *=-1;
        transform.position = character.transform.position + offset;


        Resist characterResist;


        characterResist = CalculateResist(character, type);

        DisplayEffect(type, characterResist, value);

        StartCoroutine(DmgMoveCoroutine());

    }

    IEnumerator DmgMoveCoroutine()
    {
        float duration = 0.2f;
        float time = 0.0f;


        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            transform.position = target.position + offset + new Vector3(0, Mathf.Lerp(0, downHeight, linearT));

            yield return null;
        }
    }

    public abstract void DisplayEffect(DiceType type, Resist characterResist, int value);


}
