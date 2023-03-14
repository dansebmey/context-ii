using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    public CinemaBarOverlay cinemaBarOverlay;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cinemaBarOverlay = FindObjectOfType<CinemaBarOverlay>();
    }

    public void SetPerspective(Transform tx, bool showCinemaBars)
    {
        transform.SetParent(tx);
        transform.localPosition = Vector3.zero;
        transform.rotation = tx.rotation;
        
        if (showCinemaBars) cinemaBarOverlay.Show();
        else cinemaBarOverlay.Hide();
    }
}
