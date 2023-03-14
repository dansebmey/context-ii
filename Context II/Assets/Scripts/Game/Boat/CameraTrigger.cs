using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Transform targetTx;
    public bool showCinemaBars;
    
    private void OnTriggerEnter(Collider other)
    {
        Boat boat = other.GetComponent<Boat>();
        if (boat)
        {
            boat.GetCameraController().SetPerspective(targetTx, showCinemaBars);
        }
    }
}
