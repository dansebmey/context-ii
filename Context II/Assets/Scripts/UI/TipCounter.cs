using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipCounter : MonoBehaviour
{
    private TMP_Text label;
    private int balance = 0;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
    }

    public void AddToCounter(int amount)
    {
        StartCoroutine(UpdateLabel(amount));
    }

    private IEnumerator UpdateLabel(int tippedAmount)
    {
        int startAmount = balance;
        while (balance != startAmount + tippedAmount)
        {
            if (tippedAmount < 0) balance--;
            else if (tippedAmount > 0) balance++;

            label.text = "<color=#FFFFFF>Tip:</color> €" + balance;
            
            yield return new WaitForSeconds(0.1f);
        }
    }
}
