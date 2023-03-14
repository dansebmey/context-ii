using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaBarOverlay : MonoBehaviour
{
    private Animator animator;
    private bool areBarsShowing;
    
    public bool startShowing = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (startShowing) Show();
    }

    public void Show()
    {
        if (areBarsShowing) return;
        
        areBarsShowing = true;
        animator.Play("cinema_bars_appear");
    }

    public void Hide()
    {
        if (!areBarsShowing) return;
        
        areBarsShowing = false;
        animator.Play("cinema_bars_disappear");
    }
}
