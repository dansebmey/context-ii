using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionUI : MonoBehaviour
{
    [Header("Components")]
    public Image barFill;
    public Image moodImage;
    private List<AdvancedCardSuitIcon> suitIcons;
    private TMP_Text pendingTipLabel;
    
    [Header("References")]
    public Sprite moodIcon1Frustrated;
    public Sprite moodIcon2NotHappy;
    public Sprite moodIcon3Content;
    public Sprite moodIcon4Happy;
    public Sprite moodIcon5Excited;
    public Sprite suitPlaceholderIcon;
    
    private int previousHappiness;

    private void Awake()
    {
        suitIcons = GetComponentsInChildren<AdvancedCardSuitIcon>().ToList();
        pendingTipLabel = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        ResetUI();
    }
    
    public void UpdateUI(Sprite suitSprite, int touristHappiness)
    {
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

    public void UpdateHappinessMeter(int touristHappiness)
    {
        barFill.fillAmount = touristHappiness * 0.1f;
        previousHappiness = touristHappiness;
        // StartCoroutine(FillBar(touristHappiness));
        
        if (touristHappiness >= 8) moodImage.sprite = moodIcon5Excited;
        else if (touristHappiness >= 5) moodImage.sprite = moodIcon3Content;
        else moodImage.sprite = moodIcon2NotHappy;
    }

    public void ResetUI()
    {
        UpdateHappinessMeter(4);
        previousHappiness = 4;
        
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

    public void SetPendingTip(int tip, string colourHex)
    {
        // if (tip == 0) pendingTipLabel.text = "";
        // else pendingTipLabel.text = "<color=" + colourHex + ">€" + tip + "</color>";
    }
}
