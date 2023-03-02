using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSuitIcon : MonoBehaviour
{
    private Image image;
    private Animator animator;
    private NewCardUI newCardUI;

    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        newCardUI = GetComponentInParent<NewCardUI>();
    }

    public void AssignHeart()
    {
        image.sprite = newCardUI.heartIcon;
        animator.Play("suit_icon_appear");
    }

    public void AssignBulb()
    {
        image.sprite = newCardUI.bulbIcon;
        animator.Play("suit_icon_appear");
    }

    public void AssignFist()
    {
        image.sprite = newCardUI.fistIcon;
        animator.Play("suit_icon_appear");
    }

    public void AssignCloud()
    {
        image.sprite = newCardUI.cloudIcon;
        animator.Play("suit_icon_appear");
    }

    public void ResetIcon()
    {
        image.sprite = newCardUI.placeholderIcon;
    }
}
