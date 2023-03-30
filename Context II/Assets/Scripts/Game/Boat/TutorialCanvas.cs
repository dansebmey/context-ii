using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvas : MonoBehaviour
{
    private Boat boat;
    
    private List<TutorialTextPanel> textPanels;
    public Button continueButton;
    private int currentPanelIndex;
    private Animator animator;

    private void Awake()
    {
        boat = FindObjectOfType<Boat>();
        
        textPanels = GetComponentsInChildren<TutorialTextPanel>().ToList();
        animator = GetComponent<Animator>();
        // continueButton = GetComponentInChildren<Button>();
    }
    
    private void Start()
    {
        continueButton.interactable = false;
    }

    public void StartTutorial()
    {
        boat.isMoving = false;
        
        animator.Play("tutorialCanvas_titleFadeIn");
        currentPanelIndex = 0;
        StartCoroutine(PlayAnimDelayed());
    }

    private IEnumerator PlayAnimDelayed()
    {
        yield return new WaitForSeconds(2);
        textPanels[currentPanelIndex].Play();
    }

    public void Button_OnContinue()
    {
        continueButton.interactable = false;
        textPanels[currentPanelIndex].Hide();
    }

    public void ToNextSlide()
    {
        currentPanelIndex++;
        if (currentPanelIndex < textPanels.Count)
        {
            textPanels[currentPanelIndex].Play();
        }
        else
        {
            boat.isMoving = true;
            animator.Play("tutorialCanvas_titleFadeOut");
            FindObjectOfType<TeleportHandler>().Anim_TeleportBoat();
        }
    }
}
