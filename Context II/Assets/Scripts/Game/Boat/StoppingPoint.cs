using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoppingPoint : MonoBehaviour
{
    protected virtual void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Boat boat = other.GetComponent<Boat>();
        if (boat)
        {
            boat.StartSlowingDown(this);
        }
    }

    public abstract void OnBoatArrived(Boat boat);
}
