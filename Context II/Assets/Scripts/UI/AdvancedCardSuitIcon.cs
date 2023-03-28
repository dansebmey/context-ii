using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedCardSuitIcon : CardSuitIcon
{
    [SerializeField] private Image checkImage;
    
    public Sprite checkmarkIcon;
    public Sprite crossmarkIcon;
    public Sprite placeholderIcon;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponentsInChildren<Image>()[0];
    }
    
    public void ApplyCheckmark()
    {
        checkImage.sprite = checkmarkIcon;
    }

    public void ApplyCrossmark()
    {
        checkImage.sprite = crossmarkIcon;
    }
    
    public void ResetCheckIcon()
    {
        checkImage.sprite = placeholderIcon;
    }

    public void CopyCheckmarkFrom(AdvancedCardSuitIcon suitIcon)
    {
        checkImage.sprite = suitIcon.checkImage.sprite;
    }

    public void PlaySlideInAnimation()
    {
        animator.Play("suit_icon_slideFromRight");
    }
}
