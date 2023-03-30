using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfSceneCanvas : MonoBehaviour
{
    public Image fadeOutOverlay;
    public TMP_Text salaryLabel;
    public TMP_Text tipsLabel;
    public TMP_Text rentCostsLabel;
    public TMP_Text foodCostsLabel;
    public TMP_Text balanceLabel;
    private Button continueButton;
    
    private Animator animator;
    private Boat boat;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boat = FindObjectOfType<Boat>();
        continueButton = GetComponentInChildren<Button>();
    }
    
    private void Start()
    {
        continueButton.gameObject.SetActive(false);
        continueButton.interactable = true;
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
        int salary = 80;
        int tips = boat.CollectTipsFromTourists();
        
        int rent = 50;
        int food = 40;
        
        int visualTotal;
        
        for (int i = 0; i <= salary; i++)
        {
            visualTotal = i;
            salaryLabel.text = "+ " + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i <= tips; i++)
        {
            visualTotal = salary + i;
            tipsLabel.text = "+ " + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i <= rent; i++)
        {
            visualTotal = salary + tips - i;
            rentCostsLabel.text = "- " + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i <= food; i++)
        {
            visualTotal = salary + tips - rent - i;
            foodCostsLabel.text = "- " + i;
            SetBalanceLabelText(visualTotal);
            
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);

        ShowContinueButton();
    }

    private void ShowContinueButton()
    {
        continueButton.gameObject.SetActive(true);
    }

    public void Button_ToNextScene()
    {
        continueButton.interactable = false;
        animator.Play("eosCanvas_toNextScene");
    }

    public void Anim_ToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void SetBalanceLabelText(int visualTotal)
    {
        if (visualTotal < 0)
        {
            balanceLabel.text = "<color=#E65553>- €" + -visualTotal + "</color>";
        }
        else balanceLabel.text = "<color=#FFFFFF>€" + visualTotal + "</color>";
    }
}
