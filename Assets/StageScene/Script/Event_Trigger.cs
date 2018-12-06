using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
namespace StageScene {
    public abstract class Event_Trigger : MonoBehaviour {
        protected Subject<bool> TalkEvent = new Subject<bool>();
        public IObservable<bool> Event {
            get {
                return TalkEvent;
            }
        }

        protected IObservable<Unit> waitForFire; 
        void Start() {
            waitForFire = this.UpdateAsObservable()
            .Where(_ => Input.GetButtonDown("Fire1")).First();
        }

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Alice") {
                StartCoroutine(gameEvent());
            }
        }

        protected abstract IEnumerator gameEvent();
    }
}