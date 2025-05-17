using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public SpriteRenderer arrowStart;
    public SpriteRenderer arrowEnd;
    public LineRenderer lr;

    [SerializeField]
    private Color clashingColor;
    [SerializeField]
    private Color oneWayEnemyColor;
    [SerializeField]
    private Color oneWayTeamColor;

    public Transform diceTransform;

    private bool isChoosingCard;

    public void InitPosition(Transform transform)
    {
        diceTransform = transform;
    }


    private void Awake()
    {
        HideArrow();
    }

    private void OnEnable()
    {
        ResetPos();
    }

    private void ResetPos()
    {
        arrowStart.transform.position = diceTransform.position;
    }

    public void DisplayArrow()
    {
        gameObject.SetActive(true);
    }

    public void HideArrow()
    {
        gameObject.SetActive(false);
    }

    private void SetArrowPos(Transform target)
    {
        ResetPos();


        Vector3 startPos = arrowStart.transform.position;
        Vector3 endPos = target.position;

        endPos.z = -10;

        Vector3 center = (startPos + endPos) * 0.5f;

        center.y += 20;

        startPos += center;
        endPos += center;


        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));

            point -= center;

            point.z = 0;

            lr.SetPosition(i, point);
        }

        endPos -= center;
        endPos.z = 0;

        arrowEnd.transform.position = endPos;

        Vector3 lineCenter = lr.GetPosition(lr.positionCount / 2);

        Vector3 dir = endPos - lineCenter;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        arrowEnd.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);



    }

    public void SetArrowOneWay(Transform target, bool isEnemy)
    {

        if (isEnemy)
        {
            SetEnemyColor();
        }

        else
        {
            SetTeamColor();
        }

        SetArrowPos(target);
    }

    public void EndChoosingCard()
    {
        gameObject.SetActive(false);
        isChoosingCard = false;
    }

    private void SetTeamColor()
    {
        arrowStart.color = oneWayTeamColor;
        arrowEnd.color = oneWayTeamColor;
        lr.startColor = oneWayTeamColor;
        lr.endColor = oneWayTeamColor;
    }

    private void SetEnemyColor()
    {
        arrowStart.color = oneWayEnemyColor;
        arrowEnd.color = oneWayEnemyColor;
        lr.startColor = oneWayEnemyColor;
        lr.endColor = oneWayEnemyColor;
    }

    public void ChoosingCard()
    {
        gameObject.SetActive(true);

        SetTeamColor();

        StartCoroutine(ChoosingCardCoroutine());
    }
    IEnumerator ChoosingCardCoroutine()
    {
        isChoosingCard = true;

        while (isChoosingCard)
        {
            Vector3 startPos = arrowStart.transform.position;
            Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            endPos.z = -10;

            Vector3 center = (startPos + endPos) * 0.5f;

            center.y += 20;

            startPos += center;
            endPos += center;


            for (int i = 0; i < lr.positionCount; i++)
            {

                Vector3 point = Vector3.Slerp(startPos, endPos, i / (float)(lr.positionCount - 1));

                point -= center;

                point.z = 0;

                lr.SetPosition(i, point);
            }

            endPos -= center;
            endPos.z = 0;

            arrowEnd.transform.position = endPos;

            Vector3 lineCenter = lr.GetPosition(lr.positionCount / 2);

            Vector3 dir = endPos - lineCenter;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            arrowEnd.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

            yield return null;
        }

    }



    public void SetArrowClashing(Transform target)
    {
        arrowStart.color = clashingColor;
        arrowEnd.color = clashingColor;
        lr.startColor = clashingColor;
        lr.endColor = clashingColor;
        SetArrowPos(target);
    }

}
