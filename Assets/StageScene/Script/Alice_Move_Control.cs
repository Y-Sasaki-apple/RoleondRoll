﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Alice_Move_Control : Mover_Control {
    private float input_lr = 0.0f;

    public const float speed_jump = 10.0f;

    private bool pushed_jump = false;

    private bool do_jump = false;
    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Ground")
            f_grounded = true;
    }
    private bool is_grounded = false;
    private bool f_grounded = false;
    private bool enable_input = true;
    void Start() {
        setup();
        //game = GameObject.FindGameObjectWithTag("GameActors").GetComponent<GameEventController>();

    }
    void Update() {
        input();
        animator.SetBool("isRunning", input_lr != 0);
        animator.SetBool("isGrounded", is_grounded);
        animator.SetFloat("YSpeed", rb2d.velocity.y);
        if (pushed_jump && is_grounded) {
            animator.SetTrigger("Leap");
            do_jump = true;
        }
        face_front();
    }
    private void FixedUpdate() {
        if (enable_input) {
            if (is_grounded) walk_to_lr(input_lr);
            else fly_to_lr(input_lr);
            jump_process();
        }
        is_grounded = f_grounded;
        f_grounded = false;
    }
    private void fly_to_lr(float lr) {
        if (input_lr != 0)
            rb2d.AddForce(new Vector2(speed_walk * lr - rb2d.velocity.x, 0) * rb2d.mass / 5, ForceMode2D.Impulse);
    }
    private void input() {
        if (enable_input) {
            input_lr = Input.GetAxis("Horizontal");
            if (input_lr > 0) {
                front_way = 1;
            } else if (input_lr < 0) {
                front_way = -1;
            }
            pushed_jump = Input.GetButtonDown("Fire1");
        } else {
            input_lr = 0;
            pushed_jump = false;
        }
    }

    private void jump_process() {
        if (do_jump) {
            do_jump = false;
            rb2d.AddForce(new Vector2(0, speed_jump) * rb2d.mass, ForceMode2D.Impulse);
        }
    }
}