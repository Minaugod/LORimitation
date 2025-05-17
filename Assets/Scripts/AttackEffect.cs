using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{

    private SpriteRenderer effectSpriteRenderer;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        effectSpriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void DisplayEffect(Sprite sprite)
    {
        effectSpriteRenderer.sprite = sprite;

        animator.SetTrigger("Display");
        
    }



}
