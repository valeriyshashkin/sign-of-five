using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        animator.Play("FadeIn");
    }

    public void Hide()
    {
        animator.Play("FadeOut");
    }
}
