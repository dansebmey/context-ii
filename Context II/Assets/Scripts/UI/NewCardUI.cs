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
    }

    public void ResetIcons()
    {
        cardDescription.text = "";
        foreach (CardSuitIcon icon in cardSuitIcons)
        {
            icon.ResetIcon();
        }
    }

    public void PopUpEmotionIcons()
    {
        StartCoroutine(_PopUpEmotionIcons());
    }

    private IEnumerator _PopUpEmotionIcons()
    {
        foreach (CardSuitIcon icon in cardSuitIcons)
        {
            icon.PopUpIcon();
            yield return new WaitForSeconds(0.5f);
        }
        animator.Play("new_card_slideToDeck");
    }

    public void UpdateDescription(string description)
    {
        cardDescription.text = description;
    }

    public Sprite AddHeartSuit()
    {
        suitsAdded++;
        return cardSuitIcons[suitsAdded].AssignHeart();
    }

    public Sprite AddBulbSuit()
    {
        suitsAdded++;
        return cardSuitIcons[suitsAdded].AssignBulb();
    }

    public Sprite AddFistSuit()
    {
        suitsAdded++;
        return cardSuitIcons[suitsAdded].AssignFist();
    }

    public Sprite AddCloudSuit()
    {
        suitsAdded++;
        return cardSuitIcons[suitsAdded].AssignCloud();
    }

    public void PlayAddToDeckAnimation()
    {
        // animator.Play("new_card_bringToCenter");
    }

    public Vector3 GetNewSuitPos()
    {
        return cardSuitIcons[suitsAdded].transform.position;
    }
}