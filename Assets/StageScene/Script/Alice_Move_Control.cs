using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

class Alice_Move_Control : Mover_Control {
    [SerializeField] private FloatReactiveProperty input_lr = new FloatReactiveProperty(0.0f);

    [SerializeField] public float speed_jump = 10.0f;

    private bool do_jump = false;
    public GameEventController game;

    private BoolReactiveProperty is_grounded = new BoolReactiveProperty(false);
    private bool enable_input = true;
    void Start() {
        setup();

        game = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEventController>();
        game.talkEvent.Subscribe(talk => freezed.Value = talk).AddTo(gameObject);

        FreezableUpdate.Subscribe(_ => {
            input_lr.Value = enable_input ? Input.GetAxis("Horizontal") : 0;
            animator.SetFloat("YSpeed", rb2d.velocity.y);
        });
        input_lr.Subscribe(i => {
            animator.SetBool("isRunning", input_lr.Value != 0);
            front_way = input_lr.Value != 0 ? input_lr.Value.CompareTo(0) : front_way;
        });

        this.OnTriggerStay2DAsObservable().Where(c => c.tag == "Ground").ThrottleFrame(1).Subscribe(_ => is_grounded.Value = false);
        this.OnTriggerStay2DAsObservable().Where(c => c.tag == "Ground").Subscribe(_ => is_grounded.Value = true);
        is_grounded.Subscribe(i => animator.SetBool("isGrounded", i));

        FreezableUpdate.Where(_ => Input.GetButtonDown("Fire1")).Where(_ => is_grounded.Value).Subscribe(p => {
            animator.SetTrigger("Leap");
            Observable.NextFrame(FrameCountType.FixedUpdate).Subscribe(_ => rb2d.AddForce(new Vector2(0, speed_jump) * rb2d.mass, ForceMode2D.Impulse));
        });
        FreezableFixedUpdate.Where(_ => is_grounded.Value).Subscribe(_ => walk_to_lr(input_lr.Value));
        FreezableFixedUpdate.Where(_ => !is_grounded.Value).Subscribe(_ => fly_to_lr(input_lr.Value));
    }

    private void fly_to_lr(float lr) {
        if (input_lr.Value != 0)
            rb2d.AddForce(new Vector2(speed_walk * lr - rb2d.velocity.x, 0) * rb2d.mass / 5, ForceMode2D.Impulse);
    }
}
