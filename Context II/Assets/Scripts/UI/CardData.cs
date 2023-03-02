using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    private string description;
    private Vector4 suitValues;

    public CardData(string story)
    {
        description = story;
        suitValues.x = 0;
        suitValues.y = 0;
        suitValues.z = 0;
        suitValues.w = 0;
    }
    
    public CardData(string story, int hearts, int brains, int fists, int thunders)
    {
        description = story;
        suitValues.x = hearts;
        suitValues.y = brains;
        suitValues.z = fists;
        suitValues.w = thunders;
    }

    public string GetDescription()
    {
        return description;
    }

    public Vector4 GetSuitValues()
    {
        return suitValues;
    }

    public void AddSuits(int hearts, int brains, int fists, int thunders)
    {
        suitValues.x += hearts;
        suitValues.y += brains;
        suitValues.z += fists;
        suitValues.w += thunders;
    }
}
