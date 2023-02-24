using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private CardData cardData;
    private Image[] suitIcons;
    
    public CardData GetCardData()
    {
        return cardData;
    }
}
