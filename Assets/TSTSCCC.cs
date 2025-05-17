using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSTSCCC : MonoBehaviour
{
    public Character character;
    void Update()
    {
        transform.position = character.transform.position;// Camera.main.WorldToViewportPoint(character.transform.position);
    }
}
