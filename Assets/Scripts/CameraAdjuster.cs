using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdjuster : MonoBehaviour
{
    public int desiredHorizontalGridCount = 20; // Set your desired horizontal grid count
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        cam.orthographicSize = (desiredHorizontalGridCount / aspectRatio) / 2f;
    }

    void Update()
    {
        // Optional: Adjust if window size changes during gameplay
        if (Screen.width != Screen.width || Screen.height != Screen.height)
        {
            AdjustCameraSize();
        }
    }
}
