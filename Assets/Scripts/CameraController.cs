using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraMarkerLeft, cameraMarkerRight, cameraTarget;
    [SerializeField] float cameraY;
    float cameraMin, cameraMax;

    void Awake()
    {
        float cameraSize = Camera.main.orthographicSize * 2;
        cameraMin = cameraMarkerLeft.transform.position.x + cameraSize;
        cameraMax = cameraMarkerRight.transform.position.x - cameraSize;
    }

    void Update()
    {
        transform.position = new(Math.Clamp(cameraTarget.transform.position.x, cameraMin, cameraMax), cameraY, -10);
    }
}
