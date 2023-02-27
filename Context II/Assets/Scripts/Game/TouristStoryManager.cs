using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class TouristStoryManager : MonoBehaviour
{
    public Boat boat;
    private readonly List<CardData> deck = new List<CardData>();
    
    private void Awake()
    {
        EventManager<CardData>.AddListener(EventType.OnCardPlayed, PlayCard);
    }

    private void Start()
    {
        AddStoryToDeck(new CardData("Iets over vogeltjes", +1, 0, 0, 0));
        AddStoryToDeck(new CardData("Iets over wetenschap", 0, +1, 0, 0));
        AddStoryToDeck(new CardData("Iets controversieels", 0, 0, +1, +1));
    }

    private void PlayCard(CardData cardData)
    {
        foreach (Tourist tourist in boat.GetTourists())
        {
            tourist.ApplyCardEffects(cardData);
        }
    }

    // private void Update()
    // {
    //     if (Input.GetKeyUp(KeyCode.Space))
    //     {
    //         CardData cd = __GenerateCardData();
    //         Debug.Log("Generated card has values: " + cd.GetSuitValues());
    //         
    //         PlayCard(cd);
    //         
    //     }
    //
    //     if (Input.GetKeyUp(KeyCode.Return))
    //     {
    //         int totalTip = boat.CollectTipsFromTourists();
    //         Debug.Log("Total tip from tourists was [" + totalTip + "]");
    //         boat.CheckOutTourists();
    //     }
    // }
    
    private CardData __GenerateCardData()
    {
        return new CardData("Test",
            Random.Range(0, 2),
            Random.Range(0, 2),
            Random.Range(0, 2),
            Random.Range(0, 2));
    }

    public List<CardData> DrawCards(int amount)
    {
        List<CardData> deckCopy = new List<CardData>(deck);
        List<CardData> drawnCards = new List<CardData>();

        while (drawnCards.Count < amount || deckCopy.Count > 0)
        {
            CardData randomCardData = deckCopy[Random.Range(0, deckCopy.Count)];
            drawnCards.Add(randomCardData);
            
            deckCopy.Remove(randomCardData);
        }

        return drawnCards;
    }

    public void AddStoryToDeck(CardData cardData)
    {
        deck.Add(cardData);
    }
}
