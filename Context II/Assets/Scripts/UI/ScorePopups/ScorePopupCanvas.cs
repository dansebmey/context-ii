using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopupCanvas : MonoBehaviour
{
    private Image icon;
    private TMP_Text pointLabel;

    private void Awake()
    {
        icon = GetComponentInChildren<Image>();
        pointLabel = GetComponentInChildren<TMP_Text>();
    }

    public void AssignTo(Sprite sprite, int pointValue)
    {
        if (sprite)
        {
            icon.sprite = sprite;   
        }
        else
        {
            icon.color = Color.clear;
        }
        
        pointLabel.text = "+" + pointValue;
        
        // float scale = Mathf.Clamp((pointValue - 10) * 0.1f, 1, 2);
        // transform.localScale = new Vector2(scale, scale);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
