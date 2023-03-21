using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandomFrame : MonoBehaviour
{
    float StartFrame;
    float AnimSpeed;

    void Start()
    {
        StartFrame = Random.Range(0f, 300f);
        AnimSpeed = Random.Range(1f, 1.2f);

        Debug.Log(StartFrame);
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.speed = AnimSpeed;
        anim.Play(state.fullPathHash, -1, StartFrame);

    }
}