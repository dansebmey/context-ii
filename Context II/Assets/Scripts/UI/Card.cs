using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    public Sprite heartIcon;
    public Sprite bulbIcon;
    public Sprite fistIcon;
    public Sprite cloudIcon;
    public Sprite placeholderIcon;

    private const int SUIT_HEARTS = 0;
    private const int SUIT_BULBS = 1;
    private const int SUIT_FISTS = 2;
    private const int SUIT_CLOUDS = 3;
    
    [Header("Components")]
    private CardSuitIcon[] suitIcons;
    private TMP_Text descriptionLabel;

    [Header("Variables")]
    [Range(1, 8)] public int maxSuits = 4;

    private CardData cardData;
    private Animator animator;
    private bool isInteractable;
    
    public enum CardID { Left, Middle, Right }
    public CardID cardID;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        descriptionLabel = GetComponentInChildren<TMP_Text>();
        suitIcons = GetComponentsInChildren<CardSuitIcon>();
    }

    public void AssignCardData(CardData data)
    {
        cardData = data;
        descriptionLabel.text = data.GetDescription();

        Vector4 suitValues = data.GetSuitValues();
        for (int i = 0; i < maxSuits; i++)
        {
            if (suitValues.x > 0)
            {
                suitIcons[i].SetSprite(heartIcon);
                suitValues.x--;
            }
            else if (suitValues.y > 0)
            {
                suitIcons[i].SetSprite(bulbIcon);
                suitValues.y--;
            }
            else if (suitValues.z > 0)
            {
                suitIcons[i].SetSprite(fistIcon);
                suitValues.z--;
            }
            else if (suitValues.w > 0)
            {
                suitIcons[i].SetSprite(cloudIcon);
                suitValues.w--;
            }
            else
            {
                suitIcons[i].SetSprite(placeholderIcon);
            }
        }

        isInteractable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable) return;
        
        EventManager<CardData, CardID>.Invoke(EventType.OnCardPicked, cardData, cardID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;
        
        animator.Play("card_hover_enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;
        
        animator.Play("card_hover_exit");
    }

    public void PlayDrawAnimation(string cardPos)
    {
        animator.Play("card_draw_" + cardPos);
        foreach (CardSuitIcon icon in suitIcons)
        {
            icon.PopUpIcon();
        }
    }

    public void Anim_MakeInteractable()
    {
        isInteractable = true;
        animator.Play("card_hover_down");
    }

    public void HideCard()
    {
        isInteractable = false;
        animator.Play("card_disappear");
    }

    public void BringToMiddle()
    {
        isInteractable = false;
        animator.Play("card_toMiddle");
    }

    public void Anim_PlaySuits()
    {
        StartCoroutine(_PlaySuits());
    }

    private IEnumerator _PlaySuits()
    {
        foreach (CardSuitIcon icon in suitIcons)
        {
            icon.PopDownIcon();
            
            if (icon.image.sprite == heartIcon) EventManager<int>.Invoke(EventType.OnSuitPlayed, SUIT_HEARTS);
            if (icon.image.sprite == bulbIcon) EventManager<int>.Invoke(EventType.OnSuitPlayed, SUIT_BULBS);
            if (icon.image.sprite == fistIcon) EventManager<int>.Invoke(EventType.OnSuitPlayed, SUIT_FISTS);
            if (icon.image.sprite == cloudIcon) EventManager<int>.Invoke(EventType.OnSuitPlayed, SUIT_CLOUDS);
            
            yield return new WaitForSeconds(0.75f);
        }
        
        animator.Play("card_discard");
        EventManager.Invoke(EventType.OnAllSuitsPlayed);
    }
}
