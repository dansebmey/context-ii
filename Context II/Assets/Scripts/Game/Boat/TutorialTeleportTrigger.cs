using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTeleportTrigger : TeleportTrigger
{
    public override void OnBoatArrived(Boat boat)
    {
        teleportHandler.PrepareTutorialTeleport(destinationTx);
    }
}
