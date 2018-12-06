using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace StageScene {
    public class Gate : MonoBehaviour {
        //public GameController game;
        private BehaviorSubject<bool> GateEvent = new BehaviorSubject<bool>(false);
        public IObservable<bool> Event {
            get {
                return GateEvent;
            }
        }
        void Start() {
            //ゲームクリア
            this.OnTriggerEnter2DAsObservable().Where(_ => !GateEvent.Value).Where(c => c.tag == "Alice").Subscribe(alice => {
                GateEvent.OnNext(true);
                StartCoroutine(Goal(alice.gameObject));
            }
            );
        }

        IEnumerator Goal(GameObject alice) {
            var k = 0.0f;
            var oldpos = alice.transform.position;
            var tarpos = transform.position - new Vector3(0, 0.5f);
            while (k < 1.0) {
                k += 0.03f;
                alice.transform.position = (oldpos * (1 - k) + tarpos * k);
                yield return null;
            }
            alice.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            k = 0;
            while (k < 1.0) {
                k += 0.1f;
                transform.localScale = new Vector3((1 - k) * 2, (1 - k) * 2, 1);
                yield return null;
            }
            GateEvent.OnNext(false);
        }

    }
}