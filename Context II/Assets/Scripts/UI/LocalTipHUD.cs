using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalTipHUD : MonoBehaviour
{
    private TMP_Text label;
    private int favour = 1;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        label.text = ":)";
    }

    public void AddToFavour(int amount)
    {
        favour += amount;

        if (favour >= 3)
        {
            label.text = ":D";
        }
        else if (favour >= 1)
        {
            label.text = ":)";
        }
        else if (favour == 0)
        {
            label.text = ":|";
        }
        else if (favour >= -2)
        {
            label.text = ":(";
        }
        else if (favour < -2)
        {
            label.text = ">:(";
        }

        // if (favour < 0)
        // {
        //     label.text = "</";
        //     for (int i = 0; i > favour; i--)
        //     {
        //         label.text += "3";
        //     }
        // }
        // else if (favour > 0)
        // {
        //     label.text = "<";
        //     for (int i = 0; i < favour; i++)
        //     {
        //         label.text += "3";
        //     }
        // }
        // else
        // {
        //     label.text = "|:";
        // }
    }
}