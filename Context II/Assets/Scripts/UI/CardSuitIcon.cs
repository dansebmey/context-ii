using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSuitIcon : MonoBehaviour
{
    [HideInInspector] public Image image;
    private Animator animator;
    private NewCardUI newCardUI;

    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        newCardUI = GetComponentInParent<NewCardUI>();
    }

    public Sprite AssignHeart()
    {
        image.sprite = newCardUI.heartIcon;
        // animator.Play("suit_icon_appear");
        
        return image.sprite;
    }

    public Sprite AssignBulb()
    {
        image.sprite = newCardUI.bulbIcon;
        // animator.Play("suit_icon_appear");
        
        return image.sprite;
    }

    public Sprite AssignFist()
    {
        image.sprite = newCardUI.fistIcon;
        // animator.Play("suit_icon_appear");
        
        return image.sprite;
    }

    public Sprite AssignCloud()
    {
        image.sprite = newCardUI.cloudIcon;
        // animator.Play("suit_icon_appear");
        
        return image.sprite;
    }

    public void PopUpIcon()
    {
        image.color = Color.white;
        animator.Play("suit_icon_appear");
    }

    public void PopDownIcon()
    {
        animator.Play("suit_icon_disappear");
    }

    public void ResetIcon()
    {
        image.color = Color.clear;
        image.sprite = newCardUI.placeholderIcon;
    }

    public void SetSprite(Sprite icon)
    {
        image.sprite = icon;
    }
}
