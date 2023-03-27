using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tourist : MonoBehaviour
{
    public Sprite chathead;
    
    private int initialTipValue;
    // This is the money you get when dropping the tourist off.

    public int maxTipValue;
    // Could be visually implicated by how wealthy the tourist looks.
    
    private const int SUPER_PASSIONATE = +2;
    private const int PASSIONATE = +1;
    private const int NEUTRAL = 0;
    private const int DESPICABLE = -1;

    private const int SUIT_HEARTS = 0;
    private const int SUIT_BULBS = 1;
    private const int SUIT_FISTS = 2;
    private const int SUIT_CLOUDS = 3;

    [Range(-1, +1)] public int heartLevel;
    [Range(-1, +1)] public int brainLevel;
    [Range(-1, +1)] public int fistLevel;

    private Vector4 suitAffinities;
    
    // Realtime variables
    private int currentTipValue;
    private int tipMultiplier = 1;
    private int lastTipBonus;
    public LocalTipHUD localTipHUD;
    private TouristPortrait portrait;

    private void Awake()
    {
        EventManager<int>.AddListener(EventType.OnSuitPlayed, OnSuitPlayed);
    }

    private void Start()
    {
        // initialTipValue = Random.Range(3, 5);
        initialTipValue = 2;
        currentTipValue = initialTipValue;

        suitAffinities.x = heartLevel;
        suitAffinities.y = brainLevel;
        suitAffinities.z = fistLevel;
        suitAffinities.w = -1;
    }

    public void ApplyCardEffects(CardData cardData)
    {
        HandleTipBonus(cardData);
    }

    private void OnSuitPlayed(int suitConst)
    {
        switch (suitConst)
        {
            case SUIT_HEARTS:
                if (suitAffinities.x > 0) portrait.AddHeart(suitAffinities.x);
                break;
            case SUIT_BULBS:
                if (suitAffinities.y > 0) portrait.AddBulb(suitAffinities.y);
                break;
            case SUIT_FISTS:
                if (suitAffinities.z > 0) portrait.AddFist(suitAffinities.z);
                break;
            case SUIT_CLOUDS:
                if (suitAffinities.w > 0) portrait.AddCloud();
                break;
        }
    }

    private void HandleTipBonus(CardData cardData)
    {
        int tipBonus = (int)Vector4.Dot(cardData.GetSuitValues(), suitAffinities);
        currentTipValue += tipBonus;
        Debug.Log("Tourist [" + gameObject.name + "] gave a tip bonus of [" + tipBonus + "] based on the card [" + cardData.GetSuitValues() + "]");
        
        if ((tipBonus > 0 && lastTipBonus > 0) || (tipBonus < 0 && lastTipBonus < 0))
        {
            tipMultiplier += 1;
        }
        else if (tipBonus < 0 || tipBonus > 0)
        {
            tipMultiplier = 1;
        }
        
        lastTipBonus = tipBonus;
        localTipHUD.AddToFavour(tipBonus);
    }

    public int GetTipFromCheckout()
    {
        int finalTip = Math.Clamp(currentTipValue * tipMultiplier, 0, maxTipValue);
        Debug.Log("Tourist [" + gameObject.name + "] gave a total tip of [" + finalTip + "]");
        
        return finalTip;
    }

    public void SetPortrait(TouristPortrait touristPortrait)
    {
        portrait = touristPortrait;
    }
}
