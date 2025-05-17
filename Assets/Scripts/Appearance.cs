using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
public class Appearance : MonoBehaviour
{

    public GameObject Default;
    public GameObject Move;
    public GameObject Damaged;
    public GameObject Evade;
    public GameObject Guard;
    public GameObject Slash;
    public GameObject Penetrate;
    public GameObject Hit;

    private Dictionary<CharacterState, GameObject> stateDic = new Dictionary<CharacterState, GameObject>();

    public CharacterState state;

    [SerializeField]
    private SpriteRenderer[] damagedSpriteRenderer;

    private void Awake()
    {
        // 캐릭터의 행동에 따른 외형 딕셔너리에 추가
        stateDic.Add(CharacterState.Default, Default);
        stateDic.Add(CharacterState.Move, Move);
        stateDic.Add(CharacterState.Damaged, Damaged);
        stateDic.Add(CharacterState.Evade, Evade);
        stateDic.Add(CharacterState.Guard, Guard);
        stateDic.Add(CharacterState.Slash, Slash);
        stateDic.Add(CharacterState.Pierce, Penetrate);
        stateDic.Add(CharacterState.Blunt, Hit);

        StateChange(CharacterState.Default);
    }


    public void TakeAttack()
    {
        StartCoroutine(BlinkRedCoroutine());
    }


    IEnumerator BlinkRedCoroutine()
    {
        StateChange(CharacterState.Damaged);

        transform.localRotation = Quaternion.Euler(0, 0, -10);

        foreach (SpriteRenderer spriteRenderer in damagedSpriteRenderer)
        {
            spriteRenderer.material.color = Color.red;

        }

        yield return new WaitForSeconds(0.2f);

        transform.localRotation = Quaternion.Euler(0, 0, 0);

        foreach (SpriteRenderer spriteRenderer in damagedSpriteRenderer)
        {
            spriteRenderer.material.color = Color.white;

        }


        yield return null;
    }

    public void StateChange(CharacterState state)
    {

        stateDic[this.state].SetActive(false);
        stateDic[state].SetActive(true);

        this.state = state;

    }

    public void HideCharacter()
    {
        stateDic[state].SetActive(false);
    }

}
