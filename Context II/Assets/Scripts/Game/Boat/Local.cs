using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Local : StoppingPoint
{
    [Header("References")]
    public NewCardUI newCardUI;
    
    [Header("Components")]
    public DialogueSystemTrigger dialogTrigger;

    [Header("Variables")]
    public string conversationTitle;

    public override void OnBoatArrived(Boat boat)
    {
        dialogTrigger.startConversationEntryTitle = conversationTitle;
        dialogTrigger.OnUse();
        newCardUI.ShowNewCard();
    }
}
