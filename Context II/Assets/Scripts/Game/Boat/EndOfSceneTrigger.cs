using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfSceneTrigger : StoppingPoint
{
    private EndOfSceneCanvas endOfSceneCanvas;

    private void Awake()
    {
        endOfSceneCanvas = FindObjectOfType<EndOfSceneCanvas>(true);
    }

    public override void OnBoatArrived(Boat boat)
    {
        endOfSceneCanvas.WrapUp();
    }
}
