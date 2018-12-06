using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class FadeTransition : MonoBehaviour {
    [SerializeField] private int maxcount;
    private Material material;

    void Start() {
        material = GetComponent<Image>().material;
    }

    public IObservable<Unit> openTransition() {
        int count = 0;
        var Transition = new Subject<Unit>();
        Observable.NextFrame().Subscribe(_ =>
        {
            //他Objectの開始時に呼ばれうるので
            material.SetFloat("_AlphaD", 1);
        });

        this.UpdateAsObservable().Take(maxcount).Subscribe(_ =>
        {
            count++;
            material.SetFloat("_AlphaL", (float)count / maxcount);
        }, () =>
        {
            Transition.OnCompleted();
        }
        );
        return Transition;
    }

    public IObservable<Unit> startTransition() {
        int count = 0;
        var Transition = new Subject<Unit>();
        Observable.NextFrame().Subscribe(_ =>
        {
            material.SetFloat("_AlphaL", 0);
        });
        this.UpdateAsObservable().Take(maxcount).Subscribe(_ =>
        {
            count++;
            material.SetFloat("_AlphaD", (float)count / maxcount);
        }, () =>
        {
            Transition.OnCompleted();
        }
        );
        return Transition;
    }
}
