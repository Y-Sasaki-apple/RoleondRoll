using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Alice_Control : MonoBehaviour /*, IGameEventHandler */{
    private bool pushed_shot = false;
    private bool pushed_timestop = false;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool enable_input = true;
    private Alice_Move_Control move_Control;
    //public GameEventController game;
    public GameObject bullet;//Controlに変更して汎用化？

    void Start() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        move_Control = GetComponent<Alice_Move_Control>();
        //game = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEventController>();
        //game.TimeStopEvent.Subscribe(isstop => { Debug.Log("TTT"); return null; });
    }

    void Update() {
        input();
        if (pushed_shot) animator.SetTrigger("Shoot");
        shot_process();
        //timestop_process();
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

    private void shot_process() {
        if (pushed_shot) {
            var b = Instantiate(bullet, rb2d.transform.position + new Vector3(0, 0.5f), rb2d.transform.rotation);
            b.GetComponent<Bullet_Control>().velocity = new Vector3(move_Control.front_way * 5, 0);
            b.transform.parent = transform.parent;
        }
    }

    private void input() {
        if (enable_input) {
            pushed_shot = Input.GetButtonDown("Fire2");
            pushed_timestop = Input.GetButtonDown("Fire3");
        } else {
            pushed_shot = false;
            pushed_timestop = false;
        }
    }

    private void timestop_process() {
        if (pushed_timestop) {
            //game.try_timeStop();
        }
    }

    IEnumerator invincible_process() {
        gameObject.layer = LayerMask.NameToLayer("Player_Invincible");
        yield return new WaitForSeconds(3.0f);
        gameObject.layer = LayerMask.NameToLayer("Player");
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
