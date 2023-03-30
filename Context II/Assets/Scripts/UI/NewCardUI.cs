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

    private int suitsAdded = 0;
    private TouristStoryManager tsManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        cardDescription = GetComponentInChildren<TMP_Text>();
        cardSuitIcons = GetComponentsInChildren<CardSuitIcon>();
        tsManager = FindObjectOfType<TouristStoryManager>();
    }
    
    public void ShowNewCard()
    {
        animator.Play("new_card_appear");
    }

    public void ResetIcons()
    {
        suitsAdded = 0;
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
        return cardSuitIcons[suitsAdded - 1].AssignHeart();
    }

    public Sprite AddBulbSuit()
    {
        suitsAdded++;
        return cardSuitIcons[suitsAdded - 1].AssignBulb();
    }

    public Sprite AddFistSuit()
    {
        suitsAdded++;
        return cardSuitIcons[suitsAdded - 1].AssignFist();
    }

    public Sprite AddCloudSuit()
    {
        suitsAdded++;
        return cardSuitIcons[suitsAdded - 1].AssignCloud();
    }

    public void PlayAddToDeckAnimation()
    {
        tsManager.CreateCardFromCache();
    }

    public void AddSuitToCard(int suitConst)
    {
        switch (suitConst)
        {
            case 1: tsManager.AddHeartsToCard(1);
                break;
            case 2: tsManager.AddBulbsToCard(1);
                break;
            case 3: tsManager.AddFistsToCard(1);
                break;
            case 4: tsManager.AddCloudsToCard(1);
                break;
        }
    }

    public void Anim_HideSuitIcons()
    {
        foreach (CardSuitIcon icon in cardSuitIcons)
        {
            icon.PopDownIcon();
        }
    }

    public Vector3 GetNewSuitPos()
    {
        return cardSuitIcons[suitsAdded].transform.position;
    }
}
