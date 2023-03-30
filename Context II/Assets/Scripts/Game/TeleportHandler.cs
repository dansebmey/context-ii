using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    private Animator animator;
    private Boat boat;
    private Transform destinationTx;

    public TutorialCanvas tutorialCanvas;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        boat = FindObjectOfType<Boat>();
        tutorialCanvas = FindObjectOfType<TutorialCanvas>();
    }

    public void PrepareTeleport(Transform tx)
    {
        destinationTx = tx;
        animator.Play("teleport_fade_out");
    }

    public void PrepareTutorialTeleport(Transform tx)
    {
        destinationTx = tx;
        animator.Play("tutorial_start");
    }

    public void Anim_TeleportBoat()
    {
        boat.Teleport(destinationTx);
        animator.Play("teleport_fade_in");
    }

    public void Anim_StartTutorial()
    {
        tutorialCanvas.StartTutorial();
        // boat.Teleport(destinationTx);
        // animator.Play("teleport_fade_in");
    }
}
