using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPromptUI : MonoBehaviour
{
    private TouristStoryManager tsManager;
    private Canvas canvas;
    
    public Card leftCardUI;
    public Card middleCardUI;
    public Card rightCardUI;

    private void Awake()
    {
        tsManager = FindObjectOfType<TouristStoryManager>();
        canvas = GetComponentInChildren<Canvas>();
        
        EventManager.AddListener(EventType.TriggerStoryPrompt, ShowPrompt);
        EventManager<CardData>.AddListener(EventType.OnCardPlayed, HidePrompt);
    }

    private void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    private void ShowPrompt()
    {
        canvas.gameObject.SetActive(true);
        
        List<CardData> drawnCards = tsManager.DrawCards(3);
        leftCardUI.AssignCardData(drawnCards[0]);
        middleCardUI.AssignCardData(drawnCards[1]);
        rightCardUI.AssignCardData(drawnCards[2]);
    }

    private void HidePrompt(CardData _)
    {
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        EventManager.RemoveListener(EventType.TriggerStoryPrompt, ShowPrompt);
    }
}
