using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Mover_Control : MonoBehaviour {
    protected Rigidbody2D rb2d;
    protected Animator animator;

    public float speed_walk = 3.0f;

    public int front_way = 1;

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
}
