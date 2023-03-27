using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryPromptUI : MonoBehaviour
{
    private TouristStoryManager tsManager;
    public Transform subCanvas;
    
    public Card leftCardUI;
    public Card middleCardUI;
    public Card rightCardUI;

    private Animator animator;

    private List<TouristPortrait> touristPortraits;

    private void Awake()
    {
        tsManager = FindObjectOfType<TouristStoryManager>();
        animator = GetComponent<Animator>();
        touristPortraits = GetComponentsInChildren<TouristPortrait>().ToList();
        
        EventManager<Boat>.AddListener(EventType.TriggerStoryPrompt, ShowPrompt);
        EventManager<CardData, Card.CardID>.AddListener(EventType.OnCardPicked, OnCardPlayed);
        EventManager.AddListener(EventType.OnAllSuitsPlayed, HidePrompt);
    }

    private void Start()
    {
        // subCanvas.gameObject.SetActive(false);
    }

    public void Anim_DrawCards()
    {
        StartCoroutine(_DrawCards());
    }

    private IEnumerator _DrawCards()
    {
        leftCardUI.PlayDrawAnimation("left");
        yield return new WaitForSeconds(0.2f);
        middleCardUI.PlayDrawAnimation("middle");
        yield return new WaitForSeconds(0.2f);
        rightCardUI.PlayDrawAnimation("right");
    }

    private void ShowPrompt(Boat boat)
    {
        // if (subCanvas.gameObject.activeSelf) return;
        // subCanvas.gameObject.SetActive(true);
        
        List<CardData> drawnCards = tsManager.DrawCards(3);
        leftCardUI.AssignCardData(drawnCards[0]);
        middleCardUI.AssignCardData(drawnCards[1]);
        rightCardUI.AssignCardData(drawnCards[2]);
        
        animator.Play("storyPrompt_drawCards");

        List<Tourist> touristsOnBoard = boat.GetTouristsOnBoard();
        for (int i = 0; i < touristsOnBoard.Count; i++)
        {
            touristPortraits[i].AssignTo(touristsOnBoard[i]);
        }
    }

    private void OnCardPlayed(CardData cardData, Card.CardID cardID)
    {
        switch (cardID)
        {
            case Card.CardID.Left:
                leftCardUI.BringToMiddle();
                middleCardUI.HideCard();
                rightCardUI.HideCard();
                break;
            case Card.CardID.Middle:
                leftCardUI.HideCard();
                middleCardUI.BringToMiddle();
                rightCardUI.HideCard();
                break;
            case Card.CardID.Right:
                leftCardUI.HideCard();
                middleCardUI.HideCard();
                rightCardUI.BringToMiddle();
                break;
        }
    }

    private void HidePrompt()
    {
        animator.Play("storyPrompt_hide");
        // subCanvas.gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        EventManager<Boat>.RemoveListener(EventType.TriggerStoryPrompt, ShowPrompt);
        EventManager<CardData, Card.CardID>.RemoveListener(EventType.OnCardPicked, OnCardPlayed);
        EventManager.RemoveListener(EventType.OnAllSuitsPlayed, HidePrompt);
    }
}
