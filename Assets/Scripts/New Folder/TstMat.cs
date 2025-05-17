using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TstMat : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    Material material;
    private void Awake()
    {
        material = spriteRenderer.material;
        material.color = Color.red;
    }
}
