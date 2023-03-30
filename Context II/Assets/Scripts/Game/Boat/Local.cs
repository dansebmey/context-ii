using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Local : Trigger
{
    [Header("References")]
    public NewCardUI newCardUI;
    
    [Header("Components")]
    public DialogueSystemTrigger dialogTrigger;
    public Transform cameraTx;

    [Header("Variables")]
    public string conversationTitle;

    public override void OnBoatArrived(Boat boat)
    {
        if (cameraTx) boat.GetCameraController().SetPerspective(cameraTx, false);
        
        dialogTrigger.startConversationEntryTitle = conversationTitle;
        dialogTrigger.OnUse();
    }
}
