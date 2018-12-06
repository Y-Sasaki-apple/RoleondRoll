using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

namespace StageScene {
    public class MagicThings_Control : Freezable_Control {
        protected Animator animator;
        public GameController game;
        // Use this for initialization
        void Start() {
            setup();
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

            //フリーズ条件:timestop,talk
            Observable.CombineLatest(game.TimeStopEvent, game.talkEvent).Select(list => list.Any(b => b)).Subscribe(f => {
                freezed.Value = f;
            }).AddTo(gameObject);

            animator = GetComponent<Animator>();
        }
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Bullet") {
                FreezableUpdate.First().Subscribe(_ =>
                {
                    animator.SetTrigger("magiced");
                    tag = "Ground";
                });
            }
        }
    }
}