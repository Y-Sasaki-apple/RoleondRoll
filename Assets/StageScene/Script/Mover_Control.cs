using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Mover_Control : MonoBehaviour/*, IGameEventHandler */{
    protected Rigidbody2D rb2d;
    protected Animator animator;

    public float speed_walk = 3.0f;

    public int front_way = 1;

    //private Vector2 velocity_before_stop;
    protected void setup() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected void face_front() {
        animator.SetInteger("Front", front_way);
    }
    protected void walk_to_lr(float lr) {
        rb2d.AddForce(new Vector2(speed_walk * lr - rb2d.velocity.x, 0) * rb2d.mass, ForceMode2D.Impulse);
    }

    //public void eventStart() {
    //    velocity_before_stop = rb2d.velocity;
    //    rb2d.isKinematic = true;
    //    rb2d.Sleep();
    //}

    //public void eventEnd() {
    //    rb2d.isKinematic = false;
    //    rb2d.WakeUp();
    //    rb2d.velocity = velocity_before_stop;
    //}
}
