using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPromptUI : MonoBehaviour
{
    private TouristStoryManager tsManager;
    public Transform subCanvas;
    
    public Card leftCardUI;
    public Card middleCardUI;
    public Card rightCardUI;

    private void Awake()
    {
        tsManager = FindObjectOfType<TouristStoryManager>();
        
        EventManager.AddListener(EventType.TriggerStoryPrompt, ShowPrompt);
        EventManager<CardData>.AddListener(EventType.OnCardPlayed, HidePrompt);
    }

    private void Start()
    {
        subCanvas.gameObject.SetActive(false);
    }

    private void ShowPrompt()
    {
        if (subCanvas.gameObject.activeSelf) return;
        
        subCanvas.gameObject.SetActive(true);
        
        List<CardData> drawnCards = tsManager.DrawCards(3);
        leftCardUI.AssignCardData(drawnCards[0]);
        middleCardUI.AssignCardData(drawnCards[1]);
        rightCardUI.AssignCardData(drawnCards[2]);
    }

    private void HidePrompt(CardData _)
    {
        subCanvas.gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        EventManager.RemoveListener(EventType.TriggerStoryPrompt, ShowPrompt);
    }
}
