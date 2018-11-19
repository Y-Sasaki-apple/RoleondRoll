using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Alice_Control : MonoBehaviour {
    private float input_lr = 0.0f;

    private const float speed_walk = 3.0f;
    private const float speed_jump = 10.0f;

    private bool pushed_jump = false;
    private bool pushed_shot = false;

    private bool is_grounded = false;
    private bool f_grounded = false;

    private Animator animator;
    private Rigidbody2D rb2d;

    public GameObject bullet;//Controlに変更して汎用化？
    void Start() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        input_lr = Input.GetAxis("Horizontal");
        animator.SetBool("isRunning", input_lr != 0);
        if (input_lr < 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);
        if (Input.GetButtonDown("Fire1")) {
            pushed_jump = true;
            animator.SetTrigger("Leap");
        } else {
            pushed_jump = false;
        }
        if (Input.GetButtonDown("Fire2")) {
            pushed_shot = true;
            animator.SetTrigger("Shoot");
            Instantiate(bullet, rb2d.transform.position, rb2d.transform.rotation);
        } else {
            pushed_shot = false;
        }
        animator.SetBool("isGrounded", is_grounded);
        animator.SetFloat("YSpeed", rb2d.velocity.y);

    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Ground")
            f_grounded = true;
    }

    private void FixedUpdate() {
        var vel = rb2d.velocity;
        vel.x = speed_walk * input_lr;
        if (pushed_jump && f_grounded) {
            vel.y += speed_jump;
            pushed_jump = false;
        }
        is_grounded = f_grounded;
        f_grounded = false;
        rb2d.velocity = vel;
    }
}
