using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private Character character;

    private bool isMoving;

    private Vector3 spawnPos;

    public Transform bridgeTransform;

    public bool IsMoving { get { return isMoving; } }

    private bool isTeleporting;

    public bool IsTeleporting { get { return isTeleporting; } }

    private int moveSpd = 34;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        transform.position = bridgeTransform.position;
    }

    public void InitCharacterPos(CharacterBaseInfo baseInfo)
    {
        if (character.IsEnemy)
        {
            spawnPos = baseInfo.spawnPos;
            bridgeTransform.position = spawnPos;
        }

        else
        {
            spawnPos = bridgeTransform.position;
        }

        SetDefaultRotation();

        transform.position = bridgeTransform.position;

        BattleManager.Instance.onEndAct += TeleportPos;
        character.stat.onDeadCharacter += DeadCharacter;
    }

    private void SetDefaultRotation()
    {
        if (character.IsEnemy)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }



    private void DeadCharacter()
    {
        BattleManager.Instance.onEndAct -= TeleportPos;
    }

    public void MoveToTarget(Transform target)
    {
        StartCoroutine(MoveToTargetCoroutine(target));
    }

    public void Knockback(Transform attacker)
    {
        StartCoroutine(KnockbackCoroutine(attacker));
    }

    public void AttackMove(Transform target)
    {
        StartCoroutine(AttackMoveCoroutine(target));
    }

    private IEnumerator AttackMoveCoroutine(Transform target)
    {
        Vector3 evadePos = bridgeTransform.position;

        float distance = bridgeTransform.position.x - target.position.x;

        evadePos.x -= distance / 5;


        float time = 0f;
        float duration = 0.1f;

        Vector3 nowPos = bridgeTransform.position;

        while (time < duration)
        {
            time += Time.deltaTime;

            float linerT = time / duration;

            linerT = Mathf.Sin(linerT * Mathf.PI * 0.5f);

            bridgeTransform.position = Vector3.Lerp(nowPos, evadePos, linerT);

            yield return null;

        }
    }


    public void EvadeMove(Transform target)
    {
        StartCoroutine(EvadeMoveCoroutine(target));
    }

    private IEnumerator EvadeMoveCoroutine(Transform target)
    {
        Vector3 evadePos = bridgeTransform.position;

        float distance = bridgeTransform.position.x - target.position.x;

        evadePos.x += distance / 3;


        float time = 0f;
        float duration = 0.1f;

        Vector3 nowPos = bridgeTransform.position;

        while (time < duration)
        {
            time += Time.deltaTime;

            float linerT = time / duration;

            linerT = Mathf.Sin(linerT * Mathf.PI * 0.5f);

            bridgeTransform.position = Vector3.Lerp(nowPos, evadePos, linerT);

            yield return null;

        }
    }

    public void TeleportPos()
    {
        
        StartCoroutine(TeleportPosCoroutine());
    }

    private IEnumerator TeleportPosCoroutine()
    {
        isTeleporting = true;

        float runTime = 0f;
        float duration = 1f;

        Vector3 nowPos = bridgeTransform.position;

        while (runTime < duration)
        {
            runTime += Time.deltaTime;

            bridgeTransform.position = Vector3.Lerp(nowPos, spawnPos, runTime / duration);

            yield return null;

        }

        isTeleporting = false;

        SetDefaultRotation();

        yield return null;
    }

    private IEnumerator KnockbackCoroutine(Transform attacker)
    {

        Vector3 knockbackPos = bridgeTransform.position;
        knockbackPos.x += bridgeTransform.position.x - attacker.position.x;

        float runTime = 0f;
        float duration = 0.1f;

        Vector3 nowPos = bridgeTransform.position;

        while (runTime < duration)
        {
            runTime += Time.deltaTime;

            float linerT = runTime / duration;

            linerT = Mathf.Sin(linerT * Mathf.PI * 0.5f);

            bridgeTransform.position = Vector3.Lerp(nowPos, knockbackPos, linerT);

            yield return null;

        }


        yield return null;
    }

    private IEnumerator MoveToTargetCoroutine(Transform target)
    {
        isMoving = true;

        if (transform.position.x - target.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        while (Vector3.Distance(transform.position, target.position) > 1.5f && !isTeleporting)
        {
            bridgeTransform.position = Vector3.MoveTowards(transform.position, target.position, moveSpd * Time.deltaTime);

            yield return null;

        }

        isMoving = false;

    }


}
