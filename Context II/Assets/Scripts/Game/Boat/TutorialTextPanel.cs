using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextPanel : MonoBehaviour
{
    private TutorialCanvas tutorialCanvas;
    private Animator animator;

    private void Awake()
    {
        tutorialCanvas = GetComponentInParent<TutorialCanvas>();
        animator = GetComponent<Animator>();
    }

    public void Play()
    {
        animator.Play("tutorialPanel_appear");
    }

    public void Anim_OnAnimEnd()
    {
        tutorialCanvas.continueButton.interactable = true;
    }
    
    public void Hide()
    {
        animator.Play("tutorialPanel_hide");
    }
    
    public void Anim_OnPanelFadedOut()
    {
        tutorialCanvas.ToNextSlide();
    }
}
