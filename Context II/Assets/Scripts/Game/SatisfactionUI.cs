using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionUI : MonoBehaviour
{
    [Header("Components")]
    public Image barFill;
    public Image moodImage;
    private List<AdvancedCardSuitIcon> suitIcons;
    
    [Header("References")]
    public Sprite moodIcon1Frustrated;
    public Sprite moodIcon2NotHappy;
    public Sprite moodIcon3Content;
    public Sprite moodIcon4Happy;
    public Sprite moodIcon5Excited;
    public Sprite suitPlaceholderIcon;

    private int suitsPlayed = 0;
    private float previousHappiness;

    private void Awake()
    {
        suitIcons = GetComponentsInChildren<AdvancedCardSuitIcon>().ToList();
    }

    private void Start()
    {
        ResetUI();
    }
    
    public void UpdateUI(Sprite suitSprite, float touristHappiness)
    {
        suitsPlayed++;

        // move existing icons to the left
        if (suitIcons[1].image.sprite != suitPlaceholderIcon)
        {
            suitIcons[0].image.sprite = suitIcons[1].image.sprite;
            suitIcons[0].CopyCheckmarkFrom(suitIcons[1]);
            suitIcons[0].PlaySlideInAnimation();
        }

        if (suitIcons[2].image.sprite != suitPlaceholderIcon)
        {
            suitIcons[1].image.sprite = suitIcons[2].image.sprite;
            suitIcons[1].CopyCheckmarkFrom(suitIcons[2]);
        }
        
        // set new icon
        suitIcons[2].image.sprite = suitSprite;
        suitIcons[2].PopUpIcon();
        
        // add check or cross to the new icon
        if (touristHappiness > previousHappiness) suitIcons[2].ApplyCheckmark();
        else if (touristHappiness < previousHappiness) suitIcons[2].ApplyCrossmark();

        UpdateHappinessMeter(touristHappiness);
    }

    public void UpdateHappinessMeter(float touristHappiness)
    {
        barFill.fillAmount = touristHappiness;
        previousHappiness = touristHappiness;
        // StartCoroutine(FillBar(touristHappiness));
        
        if (touristHappiness >= 0.8f) moodImage.sprite = moodIcon5Excited;
        else if (touristHappiness >= 0.6f) moodImage.sprite = moodIcon4Happy;
        else if (touristHappiness >= 0.4f) moodImage.sprite = moodIcon3Content;
        else if (touristHappiness >= 0.2f) moodImage.sprite = moodIcon2NotHappy; 
        else moodImage.sprite = moodIcon1Frustrated;
    }

    public void ResetUI()
    {
        suitsPlayed = 0;
        UpdateHappinessMeter(0.4f);
        previousHappiness = 0.4f;
        
        foreach (AdvancedCardSuitIcon icon in suitIcons)
        {
            icon.image.sprite = suitPlaceholderIcon;
            icon.ResetCheckIcon();
        }
    }
    
    
    ///////////////////////////////////////////////////

    private IEnumerator FillBar(float fillAmount)
    {
        float targetFillAmt = fillAmount;
        while (Math.Abs(barFill.fillAmount - targetFillAmt) > 0.01f)
        {
            barFill.fillAmount = Mathf.Lerp(barFill.fillAmount, targetFillAmt, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
