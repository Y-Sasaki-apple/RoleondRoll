using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour {
    public GameObject focus;
    public const float AREA_MIN_X = 0+9;
    public const float AREA_MIN_Y = 0+6;
    public const float AREA_MAX_X = 50;
    public const float AREA_MAX_Y = 100;
    public const float CAMERA_REGION_X = 0;
    public const float CAMERA_REGION_Y = 2;

    private Rigidbody2D rb2d;

    public float speed;
    public float maxspeed;

    void Start() {
        focus = GameObject.FindGameObjectWithTag("Alice");
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {

    }

    private void FixedUpdate() {
        var focuspos = focus.transform.position;
        var camerapos = transform.position;
        Vector3 targetpos;
        Vector2 vel;
        float x, y;
        x = Mathf.Clamp(camerapos.x, focuspos.x - CAMERA_REGION_X, focuspos.x + CAMERA_REGION_X);
        y = Mathf.Clamp(camerapos.y, focuspos.y - CAMERA_REGION_Y, focuspos.y + CAMERA_REGION_Y);
        x = Mathf.Clamp(x, AREA_MIN_X, AREA_MAX_X);
        y = Mathf.Clamp(y, AREA_MIN_Y, AREA_MAX_Y);
        targetpos = new Vector3(x, y, camerapos.z);
        vel = (targetpos - camerapos) * speed;
        if (vel.magnitude > maxspeed) vel = vel.normalized * maxspeed;
        rb2d.AddForce((vel - rb2d.velocity) * rb2d.mass, ForceMode2D.Impulse);
    }
}
