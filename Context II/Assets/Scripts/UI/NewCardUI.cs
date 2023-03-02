using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewCardUI : MonoBehaviour
{
    [Header("References")]
    public Sprite heartIcon;
    public Sprite bulbIcon;
    public Sprite fistIcon;
    public Sprite cloudIcon;
    public Sprite placeholderIcon;

    private Animator animator;
    
    private TMP_Text cardDescription;
    private CardSuitIcon[] cardSuitIcons;

    private int suitsAdded;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        cardDescription = GetComponentInChildren<TMP_Text>();
        cardSuitIcons = GetComponentsInChildren<CardSuitIcon>();
    }
    
    public void ShowNewCard()
    {
        animator.Play("new_card_appear");
        
        cardDescription.text = "";
        foreach (CardSuitIcon suitIcon in cardSuitIcons)
        {
            suitIcon.ResetIcon();
        }
    }

    public void UpdateDescription(string description)
    {
        cardDescription.text = description;
    }

    public void AddHeartSuit()
    {
        cardSuitIcons[suitsAdded].AssignHeart();
        suitsAdded++;
    }

    public void AddBulbSuit()
    {
        cardSuitIcons[suitsAdded].AssignBulb();
        suitsAdded++;
    }

    public void AddFistSuit()
    {
        cardSuitIcons[suitsAdded].AssignFist();
        suitsAdded++;
    }

    public void AddCloudSuit()
    {
        cardSuitIcons[suitsAdded].AssignCloud();
        suitsAdded++;
    }

    public void PlayAddToDeckAnimation()
    {
        animator.Play("new_card_bringToCenter");
    }
}
