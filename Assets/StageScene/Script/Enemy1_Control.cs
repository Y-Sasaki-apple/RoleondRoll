using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class Enemy1_Control : Mover_Control {
    public GameObject bomb;
    //private Vector2 velocity_before_stop;
    public GameEventController game;

    void Start() {
        setup();
        front_way = -1;
        speed_walk = 1.0f;
        FreezableFixedUpdate.Subscribe(_ => {
            walk_to_lr(front_way);
        });

        game = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEventController>();
       Observable.CombineLatest(game.TimeStopEvent,game.talkEvent).Subscribe(list => {
           if(list[0] || list[1]) {
               freezed.Value = true;
           } else {
               freezed.Value = false;
           }
        }).AddTo(gameObject);
        game.TimeStopEvent.Subscribe(i => {
            if (i) {
                tag = "Ground";
                gameObject.layer = LayerMask.NameToLayer("Ground");
            } else {
                tag = "Enemy";
                gameObject.layer = LayerMask.NameToLayer("Enemy");
            }
        }).AddTo(gameObject);
        this.OnTriggerEnter2DAsObservable().Where(c => c.tag == "Bullet").Subscribe(collider => {
            Instantiate(bomb, rb2d.transform.position, rb2d.transform.rotation);
            Destroy(collider.gameObject);
            Destroy(gameObject);
        });
        this.OnTriggerEnter2DAsObservable().Where(c => c.tag == "Ground").Subscribe(collider => {
            front_way = -front_way;
        });
    }
}
