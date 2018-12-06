using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace StageScene {
    class Alice_Move_Control : Mover_Control {
        [SerializeField] private FloatReactiveProperty input_lr = new FloatReactiveProperty(0.0f);

        [SerializeField] public float speed_jump = 10.0f;

        private BoolReactiveProperty is_grounded = new BoolReactiveProperty(false);
        private bool enable_input = true;
        void Start() {
            setup();

            //フリーズ条件：talk
            game.talkEvent.Subscribe(f =>
            {
                freezed.Value = f;
            }).AddTo(gameObject);
            //フリーズ条件:GameClear
            game.clearEvent.Subscribe(f =>
            {
                if (f) {
                    freezed.Value = true;
                    animator.speed = 0;
                }
            }).AddTo(gameObject);

            //アニメーション遷移
            FreezableUpdate.Subscribe(_ =>
            {
                animator.SetFloat("YSpeed", rb2d.velocity.y);
            });

            //横移動
            this.UpdateAsObservable().Select(_ => Input.GetButton("DASH") && is_grounded.Value).Subscribe(b => speed_walk = b ? 5.0f : 3.0f);
            FreezableUpdate.Subscribe(_ =>
            {
                input_lr.Value = enable_input ? Input.GetAxis("Horizontal") : 0;
            });
            input_lr.Subscribe(i =>
            {
                animator.SetBool("isRunning", input_lr.Value != 0);
                front_way = input_lr.Value != 0 ? input_lr.Value.CompareTo(0) : front_way;
            });
            FreezableFixedUpdate.Subscribe(_ =>
            {
                if (is_grounded.Value) walk_to_lr(input_lr.Value);
                else fly_to_lr(input_lr.Value);
            });

            //接地判定
            this.OnTriggerStay2DAsObservable().Where(c => c.tag == "Ground").ThrottleFrame(2).Subscribe(_ => is_grounded.Value = false);
            this.OnTriggerStay2DAsObservable().Where(c => c.tag == "Ground").Subscribe(_ => is_grounded.Value = true);
            is_grounded.Subscribe(i => animator.SetBool("isGrounded", i));

            //ジャンプ
            FreezableUpdate.Where(_ => Input.GetButtonDown("Fire1")).Where(_ => is_grounded.Value).Subscribe(p =>
            {
                animator.SetTrigger("Leap");
                Observable.NextFrame(FrameCountType.FixedUpdate).Subscribe(_ => rb2d.AddForce(new Vector2(0, speed_jump) * rb2d.mass, ForceMode2D.Impulse));
            });
        }

        private void fly_to_lr(float lr) {
            if (input_lr.Value != 0)
                rb2d.AddForce(new Vector2(speed_walk * lr - rb2d.velocity.x / 2, 0) * rb2d.mass / 10, ForceMode2D.Impulse);
        }
    }
}