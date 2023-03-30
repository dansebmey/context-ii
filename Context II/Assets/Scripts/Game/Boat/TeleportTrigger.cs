using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : Trigger
{
    public Transform destinationTx;
    protected TeleportHandler teleportHandler;

    private void Awake()
    {
        teleportHandler = FindObjectOfType<TeleportHandler>();
    }

    public override void OnBoatArrived(Boat boat)
    {
        teleportHandler.PrepareTeleport(destinationTx);
    }
}
