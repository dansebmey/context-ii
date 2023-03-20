using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionSpritePopup : MonoBehaviour
{
    private Image image;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void Show(Sprite sprite)
    {
        image.sprite = sprite;
        animator.Play("chat_head_emotion_appear");
    }

    public void MoveToCard()
    {
        NewCardUI newCardUI = FindObjectOfType<NewCardUI>();
        
        Vector3 targetPos = newCardUI.GetNewSuitPos();
        StartCoroutine(_MoveToCard(targetPos));
    }

    private IEnumerator _MoveToCard(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            Vector3 prevPos = transform.position;
            transform.position = Vector3.Slerp(prevPos, targetPos, 0.1f);
            
            yield return new WaitForSeconds(0.01f);
        }
        
        animator.Play("chat_head_emotion_hidden");
    }
}
