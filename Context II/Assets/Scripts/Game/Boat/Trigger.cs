using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public bool boatSlowsDown = true;
    
    protected virtual void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Boat boat = other.GetComponent<Boat>();
        if (boat)
        {
            if (boatSlowsDown) boat.StartSlowingDown(this);
            else OnBoatArrived(boat);
        }
    }
    
    public abstract void OnBoatArrived(Boat boat);
}
