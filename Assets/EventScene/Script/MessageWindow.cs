using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace EventScene {
    public class MessageWindow : MonoBehaviour, IEventPerformer<string> {
        public int charframe;
        public GameObject windowBack;
        private Text text;
        private bool able_text = false;

        private Animator animator;

        void Start() {
            windowBack = gameObject.transform.Find("WindowBack").gameObject;
            text = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
        }

        IEnumerator showMessage(Subject<Unit> f, string message) {
            var charnum = 0;
            var framecount = 0;
            while (charnum < message.Length) {
                if (able_text) {
                    framecount++;
                    if (framecount % charframe == 0) {
                        framecount = 0;
                        charnum++;
                        text.text = message.Substring(0, charnum);
                    }
                    if (Input.GetButtonDown("Fire1")) {
                        charnum = message.Length;
                        text.text = message.Substring(0, charnum);
                    }
                }
                yield return null;
            }
            f.OnCompleted();
        }

        public IObservable<Unit> next(string variable) {
            Subject<Unit> finish = new Subject<Unit>();
            Observable.NextFrame().Subscribe(_ =>
                StartCoroutine(showMessage(finish, variable))
            ).AddTo(gameObject);
            return finish;
        }
        public IObservable<Unit> hide() {
            windowBack.GetComponent<Animator>().SetBool("Show", false);
            able_text = false;
            text.text = "";
            return Observable.Empty<Unit>();
        }

        public IObservable<Unit> show() {
            windowBack.GetComponent<Animator>().SetBool("Show", true);
            text.text = "";
            this.UpdateAsObservable().Skip(25).First().Subscribe(_ => able_text = true);
            return Observable.Empty<Unit>();
        }
    }
}