using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class Effect_Controller : MonoBehaviour, IEventPerformer<Unit> {
    Subject<Unit> finish;

    public IObservable<Unit> hide() {
        throw new System.NotImplementedException();
    }

    public IObservable<Unit> next(Unit variable) {
        throw new System.NotImplementedException();
    }

    public IObservable<Unit> show() {
        finish = new Subject<Unit>();
        GetComponent<Animator>().SetTrigger("Show");
        return finish;
    }
    public void finishShow() {
        finish.OnCompleted();
    }
}
