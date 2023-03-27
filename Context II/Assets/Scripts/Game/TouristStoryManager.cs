using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TouristStoryManager : MonoBehaviour
{
    public Boat boat;
    private readonly List<CardData> deck = new List<CardData>();
    
    public NewCardUI newCardUI;
    private CardData cachedCardData;

    private EmotionSpritePopup emotionSpritePopup;
    public Transform reactionImagesParent;
    
    [Header("In-dialog reaction images")]
    public Sprite heartIconWithBG;
    public Sprite bulbIconWithBG;
    public Sprite fistIconWithBG;
    public Sprite cloudIconWithBG;

    private void Awake()
    {
        emotionSpritePopup = FindObjectOfType<EmotionSpritePopup>();
        
        EventManager<CardData, Card.CardID>.AddListener(EventType.OnCardPicked, PlayCard);
    }
    
    private void Start()
    {
        AddStoryToDeck(new CardData("Iets over vogeltjes", +1, 0, 0, 0));
        AddStoryToDeck(new CardData("Iets over wetenschap", 0, +1, 0, 0));
        // AddStoryToDeck(new CardData("Iets controversieels", 0, 0, +1, +1));
    }

    private void PlayCard(CardData cardData, Card.CardID cardID)
    {
        foreach (Tourist tourist in boat.GetTouristsOnBoard())
        {
            tourist.ApplyCardEffects(cardData);
        }

        boat.CollectTipsFromTourists();
    }
    
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

    private void AddStoryToDeck(CardData cardData)
    {
        deck.Add(cardData);
    }

    public void NewCard(string description)
    {
        cachedCardData = new CardData(description);
        newCardUI.UpdateDescription(description);
    }

    public void AddHeartsToCard(int amount)
    {
        cachedCardData.AddSuits(amount, 0, 0, 0);
        Sprite sprite = newCardUI.AddHeartSuit();
        
        emotionSpritePopup = FindObjectOfType<EmotionSpritePopup>();
        emotionSpritePopup.Show(heartIconWithBG);
        StartCoroutine(SetDialogReactionImage(heartIconWithBG));
    }

    public void AddBulbsToCard(int amount)
    {
        cachedCardData.AddSuits(0, amount, 0, 0);
        Sprite sprite = newCardUI.AddBulbSuit();
        
        emotionSpritePopup = FindObjectOfType<EmotionSpritePopup>();
        emotionSpritePopup.Show(bulbIconWithBG);
        StartCoroutine(SetDialogReactionImage(bulbIconWithBG));
    }

    public void AddFistsToCard(int amount)
    {
        cachedCardData.AddSuits(0, 0, amount, 0);
        Sprite sprite = newCardUI.AddFistSuit();
        
        emotionSpritePopup = FindObjectOfType<EmotionSpritePopup>();
        emotionSpritePopup.Show(fistIconWithBG);
        StartCoroutine(SetDialogReactionImage(fistIconWithBG));
    }

    public void AddCloudsToCard(int amount)
    {
        cachedCardData.AddSuits(0, 0, 0, amount);
        Sprite sprite = newCardUI.AddCloudSuit();

        emotionSpritePopup = FindObjectOfType<EmotionSpritePopup>();
        emotionSpritePopup.Show(cloudIconWithBG);
        StartCoroutine(SetDialogReactionImage(cloudIconWithBG));
    }

    private IEnumerator SetDialogReactionImage(Sprite sprite)
    {
        yield return new WaitForSeconds(0.05f);
        
        Image image = reactionImagesParent.GetComponentsInChildren<Image>().Last(i => i.CompareTag("Reaction Image"));
        image.sprite = sprite;
        image.color = Color.white;
    }

    public void CreateCardFromCache()
    {
        AddStoryToDeck(cachedCardData);
        newCardUI.ShowNewCard();
    }

    private void OnDestroy()
    {
        EventManager<CardData, Card.CardID>.RemoveListener(EventType.OnCardPicked, PlayCard);
    }
}
