using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    public Sprite heartIcon;
    public Sprite brainIcon;
    public Sprite fistIcon;
    public Sprite thunderIcon;
    public Sprite placeholderIcon;
    
    [Header("Components")]
    public Image[] suitIcons;
    private TMP_Text descriptionLabel;

    [Header("Variables")]
    [Range(1, 8)] public int maxSuits = 4;

    private CardData cardData;

    private void Awake()
    {
        descriptionLabel = GetComponentInChildren<TMP_Text>();
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
                suitIcons[i].sprite = heartIcon;
                suitValues.x--;
            }
            else if (suitValues.y > 0)
            {
                suitIcons[i].sprite = brainIcon;
                suitValues.y--;
            }
            else if (suitValues.z > 0)
            {
                suitIcons[i].sprite = fistIcon;
                suitValues.z--;
            }
            else if (suitValues.w > 0)
            {
                suitIcons[i].sprite = thunderIcon;
                suitValues.w--;
            }
            else
            {
                suitIcons[i].sprite = placeholderIcon;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager<CardData>.Invoke(EventType.OnCardPlayed, cardData);
    }
}
