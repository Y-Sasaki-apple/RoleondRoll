using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

namespace StageScene {
    public class Alice_Control : Freezable_Control {
        [SerializeField] private BoolReactiveProperty is_inbincible = new BoolReactiveProperty(false);
        public Subject<Unit> DamageTrigger = new Subject<Unit>();
        private Rigidbody2D rb2d;
        private Animator animator;
        private Alice_Move_Control move_Control;
        public GameObject bullet;//Controlに変更して汎用化？

        void Start() {
            GameController game;
            bool enable_input = true;
            setup();
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            move_Control = GetComponent<Alice_Move_Control>();
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

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

            //敵に衝突してダメージ
            this.OnCollisionStay2DAsObservable()
                .Where(collision => collision.collider.gameObject.tag == "Enemy")
                .Subscribe(collision =>
                    this.UpdateAsObservable().First().Where(_ => !freezed.Value && !is_inbincible.Value).Subscribe(_ =>
                    {
                        DamageTrigger.OnNext(Unit.Default);
                        is_inbincible.Value = true;
                    })
                    );

            DamageTrigger.Subscribe(_ =>
            {
                is_inbincible.Value = true;
                animator.SetTrigger("Damaged");
                FreezableFixedUpdate.Skip(150).Take(1).Subscribe(__ => is_inbincible.Value = false).AddTo(this);
                game.life.Value--;
            }).AddTo(gameObject);

            //射撃
            FreezableUpdate.Where(_ => enable_input).Where(_ => Input.GetButtonDown("Fire2"))
                .Subscribe(p =>
                {
                    var b = Instantiate(bullet, rb2d.transform.position + new Vector3(0, 0.5f), rb2d.transform.rotation);
                    b.GetComponent<Bullet_Control>().velocity = new Vector3(move_Control.front_way * 10, 0);
                    b.transform.parent = transform.parent;
                    animator.SetTrigger("Shoot");
                });

            //時間停止
            FreezableUpdate.Where(_ => enable_input).Where(_ => Input.GetButtonDown("Fire3"))
                .Subscribe(p =>
                {
                    game.try_timeStop();
                });

            //無敵処理
            is_inbincible.Where(p => p).Subscribe(i => gameObject.layer = LayerMask.NameToLayer("Player_Invincible"));
            is_inbincible.Where(p => !p).Subscribe(i => gameObject.layer = LayerMask.NameToLayer("Player"));
        }
    }
}