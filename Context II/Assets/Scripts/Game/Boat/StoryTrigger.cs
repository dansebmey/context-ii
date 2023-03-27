using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : StoppingPoint
{
    private Boat boat;
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     Boat boat = other.GetComponent<Boat>();
    //     if (boat)
    //     {
    //         EventManager.Invoke(EventType.TriggerStoryPrompt);
    //     }
    // }

    private void Awake()
    {
        EventManager.AddListener(EventType.OnAllSuitsPlayed, ResumeTrip);
        boat = FindObjectOfType<Boat>();
    }

    public override void OnBoatArrived(Boat _boat)
    {
        EventManager<Boat>.Invoke(EventType.TriggerStoryPrompt, _boat);
    }

    private void ResumeTrip()
    {
        boat.StartSpeedingUp(this);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(EventType.OnAllSuitsPlayed, ResumeTrip);
    }
}
