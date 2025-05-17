using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TstRail : MonoBehaviour
{


    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private AnimationCurve x;


    [SerializeField]
    Vector3 startPos;

    [SerializeField]
    Vector3 endPos;

    public void ASD()
    {
        gameObject.SetActive(false);

        transform.position = startPos;

        gameObject.SetActive(true);

        StartCoroutine("IEFlight");
    }

    private IEnumerator IEFlight()
    {



        float duration = 0.5f;
        float time = 0.0f;
        Vector3 start = startPos;
        Vector3 end = endPos;



        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            float heightT = curve.Evaluate(linearT);

            float hor = x.Evaluate(linearT);

            float X = Mathf.Lerp(start.x, end.x, hor);
            float height = Mathf.Lerp(0, 4f, heightT);

            transform.position = Vector2.Lerp(start, end, hor) + new Vector2(0, height);

            yield return null;
        }

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ASD();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f);
        }
    }
}
