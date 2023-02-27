using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    private string description;
    private Vector4 suitValues;

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
}
