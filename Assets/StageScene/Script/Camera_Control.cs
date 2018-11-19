using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour {
    private GameObject alice;
    private const float AREA_MIN_X = 0+9;
    private const float AREA_MIN_Y = 0+6;
    private const float AREA_MAX_X = 50;
    private const float AREA_MAX_Y = 100;
    private const float CAMERA_REGION_X = 0;
    private const float CAMERA_REGION_Y = 2;

    void Start() {
        alice = GameObject.FindGameObjectWithTag("Alice");
    }

    void Update() {
        var alicepos = alice.transform.position;
        var camerapos = transform.position;
        float x, y;
        x = Mathf.Clamp(camerapos.x, alicepos.x - CAMERA_REGION_X, alicepos.x + CAMERA_REGION_X);
        y = Mathf.Clamp(camerapos.y, alicepos.y - CAMERA_REGION_Y, alicepos.y + CAMERA_REGION_Y);
        x = Mathf.Clamp(x, AREA_MIN_X, AREA_MAX_X);
        y = Mathf.Clamp(y, AREA_MIN_Y, AREA_MAX_Y);
        transform.position = new Vector3(x, y, camerapos.z);
    }
}
