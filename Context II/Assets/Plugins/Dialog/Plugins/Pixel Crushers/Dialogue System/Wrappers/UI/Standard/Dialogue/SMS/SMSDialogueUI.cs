// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;

namespace PixelCrushers.DialogueSystem.Wrappers
{

    /// <summary>
    /// This wrapper class keeps references intact if you switch between the 
    /// compiled assembly and source code versions of the original class.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Dialogue System/UI/Standard UI/Dialogue/SMS/SMS Dialogue UI")]
    public class SMSDialogueUI : PixelCrushers.DialogueSystem.SMSDialogueUI
    {
        public override void ShowSubtitle(Subtitle subtitle)
        {
            if (subtitle.dialogueEntry.id == 0) return; // Don't need to show START entry.
            if (string.IsNullOrEmpty(subtitle.formattedText.text))
            {
                
                return;
            }
            var preDelay = subtitle.speakerInfo.IsNPC ? npcPreDelaySettings.GetDelayDuration(subtitle) : pcPreDelaySettings.GetDelayDuration(subtitle);
            if (Mathf.Approximately(0, preDelay))
            {
                AddMessage(subtitle);
            }
            else
            {
                StartCoroutine(AddMessageWithPreDelay(preDelay, subtitle));
            }
            AddRecord(subtitle);
        }
    }

}
