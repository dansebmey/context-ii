using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouristPortrait : MonoBehaviour
{
    [Header("References")]
    public Sprite heartIcon;
    public Sprite bulbIcon;
    public Sprite fistIcon;
    public Sprite cloudIcon;
    public Sprite placeholderIcon;
    
    private Image chatheadImage;
    private EmotionSpritePopup emotionSpritePopup;
    [HideInInspector] public SatisfactionUI satisfactionUI;
    
    private void Awake()
    {
        chatheadImage = GetComponent<Image>();
        Awake_RegisterResponseIcons();
        emotionSpritePopup = GetComponentInChildren<EmotionSpritePopup>();
        satisfactionUI = GetComponentInChildren<SatisfactionUI>();
    }

    private void Awake_RegisterResponseIcons()
    {
        Image[] icons = GetComponentsInChildren<Image>();
        for (int i = 2; i < icons.Length; i++)
        {
            // responseIcons.Add(icons[i]);
        }
    }

    public void AssignTo(Tourist tourist)
    {
        // GameObject chatHead = Instantiate(tourist.chatHeadPrefab, chatHeadTx, Quaternion.identity);
        chatheadImage.sprite = tourist.chathead;
        chatheadImage.color = Color.white;

        tourist.SetPortrait(this);
    }

    public void ShowResponseAnimation(string animationName)
    {
        // chatHead
    }

    public void AddSuit(int suitConst, int touristHappiness)
    {
        var sprite = suitConst switch
        {
            Tourist.SUIT_HEARTS => heartIcon,
            Tourist.SUIT_BULBS => bulbIcon,
            Tourist.SUIT_FISTS => fistIcon,
            Tourist.SUIT_CLOUDS => cloudIcon,
            _ => placeholderIcon
        };

        satisfactionUI.UpdateUI(sprite, touristHappiness);
    }
}
