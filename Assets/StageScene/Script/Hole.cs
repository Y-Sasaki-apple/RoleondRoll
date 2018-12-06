using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace StageScene {
    public class Hole : MonoBehaviour {
        public Transform returnPoint;
        
        void Start() {
            this.OnTriggerEnter2DAsObservable().Where(c => c.tag == "Alice").ThrottleFrame(2).Subscribe(collision => {
                StartCoroutine(returnposition(collision.gameObject));
            });
        }

        private IEnumerator returnposition(GameObject alice) {
            yield return new WaitForSeconds(0.5f);
            alice.GetComponent<Rigidbody2D>().velocity = new Vector2();
            alice.transform.position = returnPoint.position;
            alice.GetComponent<Alice_Control>().DamageTrigger.OnNext(Unit.Default);
        }
    }
}