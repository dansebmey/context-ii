using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfSceneCanvas : MonoBehaviour
{
    public Image fadeOutOverlay;
    public TMP_Text rentCostsLabel;
    public TMP_Text foodCostsLabel;
    public TMP_Text salaryLabel;
    public TMP_Text tipFromTourist1Label;
    public TMP_Text tipFromTourist2Label;
    public TMP_Text tipFromTourist3Label;
    public TMP_Text balanceLabel;
    
    private Animator animator;
    private Boat boat;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boat = FindObjectOfType<Boat>();
    }

    public void WrapUp()
    {
        animator.Play("eosCanvas_show");
    }

    public void Anim_CalculateBalance()
    {
        StartCoroutine(_Show());
    }
    
    private IEnumerator _Show()
    {
        int rent = 50;
        int food = 40;
        int salary = 80;
        int tipFromTourist1 = boat.CollectTipFromTourist(0);
        int tipFromTourist2 = boat.CollectTipFromTourist(1);
        int tipFromTourist3 = boat.CollectTipFromTourist(2);
        int visualTotal;
        
        for (int i = 0; i <= rent; i++)
        {
            visualTotal = -i;
            rentCostsLabel.text = "- €" + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i <= food; i++)
        {
            visualTotal = -rent -i;
            foodCostsLabel.text = "- €" + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i <= salary; i++)
        {
            visualTotal = -rent -food +i;
            salaryLabel.text = "+ €" + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= tipFromTourist1; i++)
        {
            visualTotal = -rent -food +salary +i;
            tipFromTourist1Label.text = "+ €" + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= tipFromTourist2; i++)
        {
            visualTotal = -rent -food +salary +tipFromTourist1 +i;
            tipFromTourist2Label.text = "+ €" + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i <= tipFromTourist3; i++)
        {
            visualTotal = -rent -food +salary +tipFromTourist1 +tipFromTourist2 +i;
            tipFromTourist3Label.text = "+ €" + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(4);
    }

    private void SetBalanceLabelText(int visualTotal)
    {
        if (visualTotal < 0) balanceLabel.text = "<color=#FF4242>-€" + visualTotal + "</color>";
        else balanceLabel.text = "<color=#7FFF42>+€" + visualTotal + "</color>";
    }
}
