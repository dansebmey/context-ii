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
    private int lastTipIncrease;
    public LocalTipHUD localTipHUD;
    private TouristPortrait portrait;

    private void Awake()
    {
        EventManager<int>.AddListener(EventType.OnSuitPlayed, OnSuitPlayed);
    }

    private void Start()
    {
        initialTipValue = Random.Range(2, 6);
        
        // initialTipValue = 4;
        currentTipValue = initialTipValue;

        suitAffinities.x = heartLevel;
        suitAffinities.y = brainLevel;
        suitAffinities.z = fistLevel;
        suitAffinities.w = -1;
    }

    public void ApplyCardEffects(CardData cardData)
    {
        // HandleTipBonus(cardData);
    }

    private void OnSuitPlayed(int suitConst)
    {
        int tipIncrease = 0;
        
        switch (suitConst)
        {
            case SUIT_HEARTS:
                if ((int)suitAffinities.x != 0)
                {
                    tipIncrease = (int)suitAffinities.x;
                    currentTipValue += tipIncrease;
                    
                    portrait.AddHeart(suitAffinities.x, DetermineHappiness());
                }
                break;
            case SUIT_BULBS:
                if ((int)suitAffinities.y != 0)
                {
                    tipIncrease = (int)suitAffinities.y;
                    currentTipValue += tipIncrease;
                    
                    portrait.AddBulb(suitAffinities.y, DetermineHappiness());
                }
                break;
            case SUIT_FISTS:
                if ((int)suitAffinities.z != 0)
                {
                    tipIncrease = (int)suitAffinities.z;
                    currentTipValue += tipIncrease;
                    
                    portrait.AddFist(suitAffinities.z, DetermineHappiness());
                }
                break;
            case SUIT_CLOUDS:
                if ((int)suitAffinities.w != 0)
                {
                    tipIncrease = (int)suitAffinities.w;
                    currentTipValue += tipIncrease;
                    
                    portrait.AddCloud(DetermineHappiness());
                }
                break;
        }

        if ((tipIncrease > 0 && lastTipIncrease > 0) || (tipIncrease < 0 && lastTipIncrease < 0))
        {
            tipMultiplier += 1;
        }
        else if (tipIncrease < 0 || tipIncrease > 0)
        {
            tipMultiplier = 1;
        }

        lastTipIncrease = tipIncrease;
    }

    private float DetermineHappiness()
    {
        return (1.0f / maxTipValue) * currentTipValue;
    }

    private void HandleTipBonus(CardData cardData)
    {
        int tipBonus = (int)Vector4.Dot(cardData.GetSuitValues(), suitAffinities);
        currentTipValue += tipBonus;
        Debug.Log("Tourist [" + gameObject.name + "] gave a tip bonus of [" + tipBonus + "] based on the card [" + cardData.GetSuitValues() + "]");
        
        if ((tipBonus > 0 && lastTipIncrease > 0) || (tipBonus < 0 && lastTipIncrease < 0))
        {
            tipMultiplier += 1;
        }
        else if (tipBonus < 0 || tipBonus > 0)
        {
            tipMultiplier = 1;
        }
        
        lastTipIncrease = tipBonus;
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
        portrait.satisfactionUI.UpdateHappinessMeter(DetermineHappiness());
    }
}
