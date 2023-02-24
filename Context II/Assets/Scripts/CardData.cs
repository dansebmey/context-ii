using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    // public enum Suit { Heart, Brain, Fist, Thunder }
    private Vector4 suitValues;

    public CardData(int hearts, int brains, int fists, int thunders)
    {
        suitValues.x = hearts;
        suitValues.y = brains;
        suitValues.z = fists;
        suitValues.w = thunders;
    }

    public Vector4 GetSuitValues()
    {
        return suitValues;
    }
}
