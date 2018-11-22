using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Alice_Control : Mover_Control {
    private float input_lr = 0.0f;

    public const float speed_jump = 10.0f;

    private bool pushed_jump = false;
    private bool pushed_shot = false;

    private bool do_jump = false;

    private bool is_grounded = false;
    private bool f_grounded = false;

    private bool enable_input = true;
    public GameObject bullet;//Controlに変更して汎用化？
    void Start() {
        setup();
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
        if (pushed_shot) animator.SetTrigger("Shoot");
        shot_process();
        face_front();
    }

    public void Damaged() {
        animator.SetTrigger("Damaged");
        StartCoroutine(invincible_process());
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Enemy") {
            //if (!invincible) {
            Damaged();
            //}
        }
    }
    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Ground")
            f_grounded = true;
    }

    private void FixedUpdate() {
        var vel = rb2d.velocity;
        walk_to_lr(input_lr);
        jump_process();

        is_grounded = f_grounded;
        f_grounded = false;
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
            pushed_shot = Input.GetButtonDown("Fire2");
        } else {
            input_lr = 0;
            pushed_jump = false;
            pushed_shot = false;
        }
    }

    private void shot_process() {
        if (pushed_shot) {
            var b = Instantiate(bullet, rb2d.transform.position + new Vector3(0, 0.5f), rb2d.transform.rotation);
            b.GetComponent<Bullet_Control>().velocity = new Vector3(front_way * 5, 0);
        }
    }

    private void jump_process() {
        if (do_jump) {
            do_jump = false;
            rb2d.AddForce(new Vector2(0, speed_jump) * rb2d.mass, ForceMode2D.Impulse);
        }
    }

    IEnumerator invincible_process() {
        gameObject.layer = LayerMask.NameToLayer("Player_Invincible");
        yield return new WaitForSeconds(3.0f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void startEvent() {
        enable_input = false;
    }
    public void finishEvent() {
        enable_input = true;
    }
}
