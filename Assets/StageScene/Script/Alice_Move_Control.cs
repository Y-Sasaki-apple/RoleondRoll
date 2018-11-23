using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

class Alice_Move_Control : Mover_Control {
    [SerializeField] private FloatReactiveProperty input_lr =new FloatReactiveProperty(0.0f);

    [SerializeField] public float speed_jump = 10.0f;

    private bool pushed_jump = false;

    private bool do_jump = false;

    //private void OnTriggerStay2D(Collider2D collider) {
    //    if (collider.tag == "Ground")
    //        f_grounded = true;
    //}

    private bool is_grounded = false;
    private bool f_grounded = false;
    private bool enable_input = true;
    void Start() {
        setup();
        //game = GameObject.FindGameObjectWithTag("GameActors").GetComponent<GameEventController>();
        this.UpdateAsObservable().Subscribe(_ => {
            input();
            //animator.SetBool("isRunning", input_lr != 0);
            animator.SetBool("isGrounded", is_grounded);
            animator.SetFloat("YSpeed", rb2d.velocity.y);
            if (pushed_jump && is_grounded) {
                animator.SetTrigger("Leap");
                do_jump = true;
            }
        });

        input_lr.Subscribe(i => animator.SetBool("isRunning", input_lr.Value != 0));
        this.FixedUpdateAsObservable().Subscribe(_=> {
            if (enable_input) {
                if (is_grounded) walk_to_lr(input_lr.Value);
                else fly_to_lr(input_lr.Value);
                jump_process();
            }
            is_grounded = f_grounded;
            f_grounded = false;
        });
        this.OnTriggerStay2DAsObservable().Where(c => c.tag == "Ground").Subscribe(c => {
            f_grounded = true;

        });
    }

    void Update() {

    }
    private void FixedUpdate() {

    }
    private void fly_to_lr(float lr) {
        if (input_lr.Value != 0)
            rb2d.AddForce(new Vector2(speed_walk * lr - rb2d.velocity.x, 0) * rb2d.mass / 5, ForceMode2D.Impulse);
    }
    private void input() {
        if (enable_input) {
            input_lr.Value = Input.GetAxis("Horizontal");
            if (input_lr.Value > 0) {
                front_way = 1;
            } else if (input_lr.Value < 0) {
                front_way = -1;
            }
            pushed_jump = Input.GetButtonDown("Fire1");
        } else {
            input_lr.Value = 0;
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
