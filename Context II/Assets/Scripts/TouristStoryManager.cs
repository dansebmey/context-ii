using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class TouristStoryManager : MonoBehaviour
{
    public Boat boat;
    
    public void PlayCard(CardData cardData)
    {
        foreach (Tourist tourist in boat.GetTourists())
        {
            tourist.ApplyCardEffects(cardData);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CardData cd = __GenerateCardData();
            Debug.Log("Generated card has values: " + cd.GetSuitValues());
            
            PlayCard(cd);
            
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            int totalTip = boat.CollectTipsFromTourists();
            Debug.Log("Total tip from tourists was [" + totalTip + "]");
            boat.CheckOutTourists();
        }
    }

    private CardData __GenerateCardData()
    {
        return new CardData(
            Random.Range(0, 2),
            Random.Range(0, 2),
            Random.Range(0, 2),
            Random.Range(0, 2));
    }
}
