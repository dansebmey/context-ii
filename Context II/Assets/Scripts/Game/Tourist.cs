using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tourist : MonoBehaviour
{
    public Sprite chathead;
    
    private int initialHappiness;
    private int maximumHappiness = 10;
    // This is the money you get when dropping the tourist off.

    public int tipOnContent = 2;
    public int tipOnVeryHappy = 5;
    // Could be visually implicated by how wealthy the tourist looks.
    
    private const int SUPER_PASSIONATE = +2;
    private const int PASSIONATE = +1;
    private const int NEUTRAL = 0;
    private const int DESPICABLE = -1;

    public const int SUIT_HEARTS = 0;
    public const int SUIT_BULBS = 1;
    public const int SUIT_FISTS = 2;
    public const int SUIT_CLOUDS = 3;
    private int lastSuitConst = -1;

    [Range(-1, +1)] public int heartLevel;
    [Range(-1, +1)] public int brainLevel;
    [Range(-1, +1)] public int fistLevel;

    private Vector4 suitAffinities;
    
    // Realtime variables
    private int currentHappiness;
    private int happinessBonus = 1;
    private int lastEffectOnHappiness;
    private TouristPortrait portrait;
    
    private float veryHappyThreshold = 8;
    private float contentThreshold = 5;

    private void Awake()
    {
        EventManager<int>.AddListener(EventType.OnSuitPlayed, OnSuitPlayed);
    }

    private void Start()
    {
        initialHappiness = Random.Range(2, 6);
        currentHappiness = initialHappiness;

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
        if (!this) return;
        
        switch (suitConst)
        {
            case SUIT_HEARTS:
                if ((int)suitAffinities.x != 0)
                {
                    AffectHappiness((int)suitAffinities.x + DetermineHappinessBonus(SUIT_HEARTS, suitAffinities.x));
                    lastSuitConst = SUIT_HEARTS;
                    
                    portrait.AddSuit(SUIT_HEARTS, currentHappiness);
                }
                break;
            case SUIT_BULBS:
                if ((int)suitAffinities.y != 0)
                {
                    AffectHappiness((int)suitAffinities.y + DetermineHappinessBonus(SUIT_BULBS, suitAffinities.y));
                    lastSuitConst = SUIT_BULBS;
                    
                    portrait.AddSuit(SUIT_BULBS, currentHappiness);
                }
                break;
            case SUIT_FISTS:
                if ((int)suitAffinities.z != 0)
                {
                    AffectHappiness((int)suitAffinities.z + DetermineHappinessBonus(SUIT_FISTS, suitAffinities.z));
                    lastSuitConst = SUIT_FISTS;
                    
                    portrait.AddSuit(SUIT_FISTS, currentHappiness);
                }
                break;
            case SUIT_CLOUDS:
                if ((int)suitAffinities.w != 0)
                {
                    AffectHappiness((int)suitAffinities.w + DetermineHappinessBonus(SUIT_CLOUDS, suitAffinities.w));
                    lastSuitConst = SUIT_CLOUDS;
                    
                    portrait.AddSuit(SUIT_CLOUDS, currentHappiness);
                }
                break;
        }

        string colourHex;
        if (currentHappiness > veryHappyThreshold) colourHex = "#47A254";
        else if (currentHappiness > contentThreshold) colourHex = "#FFFFFF";
        else colourHex = "#E65553";
        
        portrait.satisfactionUI.SetPendingTip(DetermineTip(currentHappiness), colourHex);

    }
    
    private void AffectHappiness(int effectOnHappiness)
    {
        currentHappiness += effectOnHappiness;
        lastEffectOnHappiness = effectOnHappiness;
        
        if (currentHappiness < 0) currentHappiness = 0;
        else if (currentHappiness > maximumHappiness) currentHappiness = maximumHappiness;
    }

    private int DetermineHappinessBonus(int suitConst, float happinessFromSuit)
    {
        if (suitConst == lastSuitConst && happinessFromSuit > 0 && lastEffectOnHappiness > 0)
        {
            if (happinessBonus < 0) happinessBonus = 0;
            return happinessBonus += 1;
        }

        if (suitConst == lastSuitConst && happinessFromSuit < 0 && lastEffectOnHappiness < 0)
        {
            if (happinessBonus > 0) happinessBonus = 0;
            return happinessBonus -= 1;
        }
        
        happinessBonus = 0;
        return happinessBonus;
    }

    private int DetermineTip(int happiness)
    {
        if (happiness >= veryHappyThreshold) return tipOnVeryHappy;
        if (happiness >= contentThreshold) return tipOnContent;
        return 0;
    }

    private float GetHappinessAsFloat(int happiness)
    {
        return 0.1f * happiness;
    }

    public int GetTipFromCheckout()
    {
        return DetermineTip(currentHappiness);
    }

    public void SetPortrait(TouristPortrait touristPortrait)
    {
        portrait = touristPortrait;
        portrait.satisfactionUI.UpdateHappinessMeter(currentHappiness);
    }
}
