using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Boat boat = other.GetComponent<Boat>();
        if (boat)
        {
            EventManager.Invoke(EventType.TriggerStoryPrompt);
        }
    }
}
