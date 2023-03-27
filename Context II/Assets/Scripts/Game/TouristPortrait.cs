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
    private readonly List<Image> responseIcons = new List<Image>();
    private EmotionSpritePopup emotionSpritePopup;
    // public Transform chatHeadTx;

    private int suitsAdded;
    
    private void Awake()
    {
        chatheadImage = GetComponent<Image>();
        Awake_RegisterResponseIcons();
        emotionSpritePopup = GetComponentInChildren<EmotionSpritePopup>();
    }

    private void Awake_RegisterResponseIcons()
    {
        Image[] icons = GetComponentsInChildren<Image>();
        for (int i = 2; i < icons.Length; i++)
        {
            responseIcons.Add(icons[i]);
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

    public void AddHeart(float suitFavourability)
    {
        suitsAdded++;
        responseIcons[suitsAdded].sprite = heartIcon;
        
        // if (suitFavourability > 0) emotionSpritePopup.Show(heartIcon);
        // else
        if (suitFavourability < 0) emotionSpritePopup.Show(cloudIcon);
    }

    public void AddBulb(float suitFavourability)
    {
        suitsAdded++;
        responseIcons[suitsAdded].sprite = bulbIcon;
        if (suitFavourability < 0) emotionSpritePopup.Show(cloudIcon);
    }

    public void AddFist(float suitFavourability)
    {
        suitsAdded++;
        responseIcons[suitsAdded].sprite = fistIcon;
        if (suitFavourability < 0) emotionSpritePopup.Show(cloudIcon);
    }

    public void AddCloud()
    {
        suitsAdded++;
        responseIcons[suitsAdded].sprite = cloudIcon;
        emotionSpritePopup.Show(cloudIcon);
    }

    public void ResetIcons()
    {
        suitsAdded = -1;
        foreach (Image icon in responseIcons)
        {
            icon.sprite = placeholderIcon;
        }
    }
}
