using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Charactor_Controller : MonoBehaviour, IEventPerformer<int> {
    Animator animator;
    Subject<Unit> finishShow;
    Subject<Unit> finishHide;

    public IObservable<Unit> hide() {
        animator.SetBool("Show", false);
        finishHide = new Subject<Unit>();
        return finishHide;
    }

    public IObservable<Unit> next(int variable) {
        animator.SetInteger("FaceID", variable);
        return Observable.Empty<Unit>();
    }

    public IObservable<Unit> show() {
        animator.SetBool("Show", true);
        finishShow = new Subject<Unit>();
        return finishShow;
    }
    public void showFinish() { finishShow.OnCompleted(); }
    public void hideFinish() { finishHide.OnCompleted(); }

    void Start () {
        animator = GetComponent<Animator>();
	}
}
