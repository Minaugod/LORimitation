using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCameraControl : MonoBehaviour
{
    

    float cameraSpeed = 0.3f;

    float horizontalLimit = 3.1f;
    float verticalMax = 3f;
    float verticalMin = -1.5f;

    Vector3 lastPosition;

    SpriteRenderer sr;
    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(2))
        {
            
            lastPosition = Input.mousePosition;
        }

        
        if (Input.GetMouseButton(2))
        {
            Vector3 movePos = Input.mousePosition - lastPosition;

            Vector3 moveDirection = new Vector3(-movePos.x, -movePos.y, -10);

            Vector3 newPosition = transform.position + moveDirection * cameraSpeed * Time.deltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimit, horizontalLimit);
            newPosition.y = Mathf.Clamp(newPosition.y, verticalMin, verticalMax);
            newPosition.z = -1;

            transform.position = newPosition;

            lastPosition = Input.mousePosition;

        }

    }
}
