using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public List<Tourist> touristsOnBoard;

    private void Start()
    {
        // touristsOnBoard = new List<Tourist>();
    }
    
    public void RegisterTourist(Tourist tourist)
    {
        touristsOnBoard.Add(tourist);
    }
    
    public List<Tourist> GetTourists()
    {
        return touristsOnBoard;
    }
    
    public int CollectTipsFromTourists()
    {
        int total = 0;
        foreach (Tourist tourist in touristsOnBoard)
        {
            total += tourist.GetTipFromCheckout();
        }
        
        return total;
    }

    public void CheckOutTourists()
    {
        touristsOnBoard.Clear();
    }
}
