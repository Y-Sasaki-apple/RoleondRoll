using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace EventScene {
    public abstract class Event_Master : MonoBehaviour {
        protected IObservable<Unit> waitForFire;
        void Start() {
            waitForFire = this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Fire1")).First();
        }

        public IObservable<Unit> startEvent() {
            Subject<Unit> e = new Subject<Unit>();
            StartCoroutine(gameEvent(e));
            return e;
        }
        protected abstract IEnumerator gameEvent(Subject<Unit> s);
    }
}