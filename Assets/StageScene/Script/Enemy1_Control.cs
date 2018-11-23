using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Control : Mover_Control/*,ITimeStopHandler*/ {
    public GameObject bomb;
    private Vector2 velocity_before_stop;

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

    //public void TimeStopStart() {
    //    tag = "Ground";
    //    gameObject.layer = LayerMask.NameToLayer("Ground");
    //    velocity_before_stop = rb2d.velocity;
    //    rb2d.isKinematic = true;
    //    rb2d.Sleep();
    //}

    //public void TimeStopEnd() {
    //    tag = "Enemy";
    //    gameObject.layer = LayerMask.NameToLayer("Enemy");
    //    rb2d.isKinematic = false;
    //    rb2d.WakeUp();
    //    rb2d.velocity = velocity_before_stop;
    //}
}
