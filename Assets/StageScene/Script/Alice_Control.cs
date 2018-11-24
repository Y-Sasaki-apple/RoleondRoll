using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Alice_Control : Freezable_Control{
    [SerializeField] private BoolReactiveProperty is_inbincible = new BoolReactiveProperty(false);
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool enable_input = true;
    private Alice_Move_Control move_Control;
    public GameEventController game;
    public GameObject bullet;//Controlに変更して汎用化？

    void Start() {
        setup();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        move_Control = GetComponent<Alice_Move_Control>();
        game = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEventController>();
        game.talkEvent.Subscribe(talk => freezed.Value = talk).AddTo(gameObject);

        this.OnCollisionStay2DAsObservable()
            .Where(collision => collision.collider.gameObject.tag == "Enemy")
            .Subscribe(collision => {
                Damaged();
            });

        FreezableUpdate.Where(_ => enable_input).Where(_ => Input.GetButtonDown("Fire2"))
            .Subscribe(p => {
                var b = Instantiate(bullet, rb2d.transform.position + new Vector3(0, 0.5f), rb2d.transform.rotation);
                b.GetComponent<Bullet_Control>().velocity = new Vector3(move_Control.front_way * 5, 0);
                b.transform.parent = transform.parent;
                animator.SetTrigger("Shoot");
            });

        FreezableUpdate.Where(_ => enable_input).Where(_ => Input.GetButtonDown("Fire3"))
            .Subscribe(p => {
                game.try_timeStop();
            });

        is_inbincible.Where(p => p).Subscribe(i => gameObject.layer = LayerMask.NameToLayer("Player_Invincible"));
        is_inbincible.Where(p => !p).Subscribe(i => gameObject.layer = LayerMask.NameToLayer("Player"));
    }

    public void Damaged() {
        animator.SetTrigger("Damaged");
        StartCoroutine(invincible_process());
    }

    IEnumerator invincible_process() {
        is_inbincible.Value = true;
        yield return new WaitForSeconds(3.0f);
        is_inbincible.Value = false;
    }

    //public new void eventStart() {
    //    base.eventStart();
    //    enable_input = false;
    //}

    //public new void eventEnd() {
    //    base.eventEnd();
    //    enable_input = true;
    //}

}
