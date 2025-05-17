using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackground : MonoBehaviour
{
    [SerializeField]
    private Transform backgroundTf;

    private Vector3 backgroundPos;

    [SerializeField]
    private GameObject battleBridgeObj;

    [SerializeField]
    private GameObject battleFrame;

    [SerializeField]
    private Animator battleBridgeAnim;

    [SerializeField]
    private float cameraChangeTime;


    private Camera normalCamera;
    private Camera battleCamera;


    private bool isCameraFollowing = false;

    private void Awake()
    {
        
        normalCamera = GameObject.Find("NormalCamera").GetComponent<Camera>();
        battleCamera = GameObject.Find("BattleCamera").GetComponent<Camera>();

        backgroundPos = backgroundTf.position;


    }

    private void Start()
    {
        battleCamera.gameObject.SetActive(false);
        //battleBridgeObj.SetActive(false);

    }

    public void ChangeToBattleBridge()
    {
        StartCoroutine(BattleBridgeCoroutine());
        StartCoroutine(ZoomInBackground());
    }

    IEnumerator ZoomInBackground()
    {


        float time = 0.0f;
        float duration = 0.3f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            backgroundTf.localScale = new Vector3(Mathf.Lerp(2, 0.8f, linearT), Mathf.Lerp(2, 0.8f, linearT), 0);


            yield return null;
        }

        yield return null;
    }

    public void ChangeToNormalBridge()
    {
        StartCoroutine(NormalBridgeCoroutine());
    }

    public void BattleCameraFollow(Transform A_Tf, Transform B_Tf)
    {
        StartCoroutine(CameraAndBgFollowCoroutine(A_Tf, B_Tf));
    }

    public void BattleCameraShake(float power)
    {
        StartCoroutine(CameraShakeCoroutine(power));
    }

    public void StopCameraFollow()
    {
        isCameraFollowing = false;
    }

    public void BattleSlowMotion()
    {
        StartCoroutine(SlowMotion());
    }

    IEnumerator SlowMotion()
    {
        float duration = 0.1f;
        float time = 0.0f;


        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            battleCamera.fieldOfView = Mathf.Lerp(65, 55, linearT);
            Time.timeScale = Mathf.Lerp(1f, 0.4f, linearT);

            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        duration = 0.1f;
        time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            battleCamera.fieldOfView = Mathf.Lerp(55, 65, linearT);
            Time.timeScale = Mathf.Lerp(0.4f, 1f, linearT);

            yield return null;
        }

        yield return null;
    }
    private IEnumerator BattleBridgeCoroutine()
    {

        float time = 0.0f;
        float duration = 0.3f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            normalCamera.orthographicSize = Mathf.Lerp(8, 2.8f, linearT);


            yield return null;
        }

        battleCamera.gameObject.SetActive(true);
        normalCamera.gameObject.SetActive(false);

        battleBridgeAnim.SetBool("Battle", true);

        time = 0.0f;
        duration = 0.3f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            battleCamera.fieldOfView = Mathf.Lerp(70, 65, linearT);

            yield return null;
        }



        yield return null;
    }

    private IEnumerator NormalBridgeCoroutine()
    {   


        backgroundTf.localScale = Vector2.one * 2;
        backgroundTf.position = backgroundPos;

        normalCamera.gameObject.SetActive(true);
        battleCamera.gameObject.SetActive(false);


        battleBridgeAnim.SetBool("Battle", false);

        float time = 0.0f;
        float duration = 0.5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float linearT = time / duration;

            normalCamera.orthographicSize = Mathf.Lerp(2, 8, linearT);

            Vector3 smoothedPosition = Vector3.Lerp(normalCamera.transform.position, Vector3.zero, linearT);
            smoothedPosition.z = -10;

            normalCamera.transform.position = smoothedPosition;


            yield return null;
        }




        yield return null;
    }


    private IEnumerator CameraAndBgFollowCoroutine(Transform A_Tf, Transform B_Tf)
    {

        isCameraFollowing = true;

        battleFrame.SetActive(true);

        while (isCameraFollowing)
        {
            Vector3 distance = (B_Tf.position - A_Tf.position) / 2;
            Vector3 targetPos = A_Tf.position + distance;

            Vector3 smoothedPosition = Vector3.Lerp(normalCamera.transform.position, targetPos, 5f * Time.deltaTime);
            smoothedPosition.z = -4;
            battleCamera.transform.position = smoothedPosition;
            normalCamera.transform.position = smoothedPosition;

            smoothedPosition.z = 0;
            backgroundTf.position = smoothedPosition;
            yield return null;
        }
        battleFrame.SetActive(false);
    }

    private IEnumerator CameraShakeCoroutine(float power)
    {
        power *= 3;

        float time = 0.0f;
        float shakeTime = 0.2f;

        float shakeIntensity = 0.15f;

        Vector3 startRotation = battleCamera.transform.eulerAngles;


        while (shakeTime > time)
        {
            time += Time.deltaTime;

            float z = Random.Range(-1, 1f);
            battleCamera.transform.rotation = Quaternion.Euler(startRotation + new Vector3(0, 0, z) * shakeIntensity * power);


            yield return null;
        }



        battleCamera.transform.rotation = Quaternion.Euler(startRotation);

    }

}
