using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Alice_Control : MonoBehaviour /*, IGameEventHandler */{
    //private bool pushed_shot = false;//obs
    [SerializeField] private BoolReactiveProperty pushed_shot = new BoolReactiveProperty(false);
    [SerializeField] private BoolReactiveProperty pushed_timestop = new BoolReactiveProperty(false);
    [SerializeField] private BoolReactiveProperty is_inbincible = new BoolReactiveProperty(false);
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool enable_input = true;
    private Alice_Move_Control move_Control;
    public GameEventController game;
    public GameObject bullet;//Controlに変更して汎用化？

    void Start() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        move_Control = GetComponent<Alice_Move_Control>();
        game = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEventController>();
        game.TimeStopEvent.Subscribe(isstop => {
            //停止
        }).AddTo(gameObject);
        this.UpdateAsObservable().Subscribe(_ => input());
        this.OnCollisionStay2DAsObservable()
            .Where(collision => collision.collider.gameObject.tag == "Enemy")
            .Subscribe(collision => Damaged());

        pushed_shot.Where(p => p).Subscribe(p => shot_process());
        pushed_shot.Where(p => p).Subscribe(p => animator.SetTrigger("Shoot"));

        pushed_timestop.Where(p => p).Subscribe(p => timestop_process());

        is_inbincible.Where(p => p).Subscribe(i => gameObject.layer = LayerMask.NameToLayer("Player_Invincible"));
        is_inbincible.Where(p => !p).Subscribe(i => gameObject.layer = LayerMask.NameToLayer("Player"));
    }

    public void Damaged() {
        animator.SetTrigger("Damaged");
        StartCoroutine(invincible_process());
    }

    private void shot_process() {
        var b = Instantiate(bullet, rb2d.transform.position + new Vector3(0, 0.5f), rb2d.transform.rotation);
        b.GetComponent<Bullet_Control>().velocity = new Vector3(move_Control.front_way * 5, 0);
        b.transform.parent = transform.parent;
    }

    private void input() {
        if (enable_input) {
            pushed_shot.Value = Input.GetButtonDown("Fire2");
            pushed_timestop.Value = Input.GetButtonDown("Fire3");
        } else {
            pushed_shot.Value = false;
            pushed_timestop.Value = false;
        }
    }

    private void timestop_process() {
        //game.try_timeStop();
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
