using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Control : Mover_Control {
    public GameObject bomb;

    void Start() {
        setup();
        front_way = -1;
        speed_walk = 1.0f;
    }

    void Update() {
        face_front();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Bullet") {
            Instantiate(bomb, rb2d.transform.position, rb2d.transform.rotation);
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        if (collider.tag == "Ground") {
            front_way = -front_way;
        }
    }

    private void FixedUpdate() {
        walk_to_lr(front_way);
    }
}
