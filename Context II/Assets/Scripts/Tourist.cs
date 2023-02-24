using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tourist : MonoBehaviour
{
    private int initialTipValue;
    // This is the money you get when dropping the tourist off.

    public int maxTipValue;
    // Could be visually implicated by how wealthy the tourist looks.
    
    private const int SUPER_PASSIONATE = +2;
    private const int PASSIONATE = +1;
    private const int NEUTRAL = 0;
    private const int DESPICABLE = -1;

    [Range(-1, +1)] public int heartLevel;
    [Range(-1, +1)] public int brainLevel;
    [Range(-1, +1)] public int fistLevel;

    private Vector4 suitAffinities;
    
    // Realtime variables
    private int currentTipValue;
    private int tipMultiplier = 1;
    private int lastTipBonus;

    private void Start()
    {
        // initialTipValue = Random.Range(3, 5);
        initialTipValue = 5;
        currentTipValue = initialTipValue;

        suitAffinities.x = heartLevel;
        suitAffinities.y = brainLevel;
        suitAffinities.z = fistLevel;
        suitAffinities.w = -1;
    }

    public void ApplyCardEffects(CardData cardData)
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
    }

    public int GetTipFromCheckout()
    {
        int finalTip = Math.Clamp(currentTipValue * tipMultiplier, 0, maxTipValue);
        Debug.Log("Tourist [" + gameObject.name + "] gave a total tip of [" + finalTip + "]");
        
        return finalTip;
    }
}
