using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StageScene {
    public class Camera_Control : MonoBehaviour {
        public Transform focus;
        public float AREA_MIN_X = 0 + 9;
        public float AREA_MIN_Y = 0 + 6;
        public float AREA_MAX_X = 50 - 9;
        public float AREA_MAX_Y = 100 - 6;
        public const float CAMERA_REGION_X = 0;
        public const float CAMERA_REGION_Y = 2;

        private Rigidbody2D rb2d;

        public float speed;
        public float maxspeed;
        public float maxlimitspeed;

        void Start() {
            focus = GameObject.FindGameObjectWithTag("Alice").transform;
            rb2d = GetComponent<Rigidbody2D>();
            var areamax = GameObject.FindGameObjectWithTag("GameArea").transform.position;
            AREA_MAX_X = areamax.x - 9;
            AREA_MAX_Y = areamax.y - 6;
        }

        void Update() {

        }

        private void FixedUpdate() {
            var focuspos = focus.position;
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
            if (vel.magnitude > maxspeed && vel.magnitude < maxlimitspeed) vel = vel.normalized * maxspeed;
            rb2d.AddForce((vel - rb2d.velocity) * rb2d.mass, ForceMode2D.Impulse);
        }
    }
}