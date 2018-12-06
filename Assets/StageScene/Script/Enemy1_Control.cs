using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
namespace StageScene {
    public class Enemy1_Control : Mover_Control {
        public GameObject bomb;

        void Start() {
            setup();
            front_way = -1;
            speed_walk = 1.0f;

            //フリーズ条件:timestop,talk
            Observable.CombineLatest(game.TimeStopEvent, game.talkEvent).Select(list=>list.Any(b=>b)).Subscribe(f => {
                freezed.Value = f;
            }).AddTo(gameObject);

            //時間停止
            game.TimeStopEvent.Subscribe(i => {
                if (i) {
                    tag = "Ground";
                    gameObject.layer = LayerMask.NameToLayer("Ground");
                    animator.enabled = false;
                } else {
                    tag = "Enemy";
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    animator.enabled = true;
                }
            }).AddTo(gameObject);

            //被弾
            this.OnTriggerEnter2DAsObservable().Where(c => c.tag == "Bullet").Subscribe(collider => {
                FreezableUpdate.First().Subscribe(_ =>
                {
                    Instantiate(bomb, rb2d.transform.position, rb2d.transform.rotation);
                    Destroy(gameObject);
                });
            });

            //移動
            FreezableFixedUpdate.Subscribe(_ => {
                walk_to_lr(front_way);
            });
            this.OnTriggerEnter2DAsObservable().Where(c => c.tag == "Ground").Subscribe(collider => {
                front_way = -front_way;
            });
        }
    }
}