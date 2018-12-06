using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace StageScene {
    public class GameController : Freezable_Control {
        private BehaviorSubject<bool> timeStop = new BehaviorSubject<bool>(false);
        private BehaviorSubject<bool> talk = new BehaviorSubject<bool>(false);
        private BehaviorSubject<bool> clear = new BehaviorSubject<bool>(false);
        public IntReactiveProperty life = new IntReactiveProperty(5);
        public IObservable<bool> TimeStopEvent {
            get {
                return timeStop;
            }
        }
        public IObservable<bool> talkEvent {
            get {
                return talk;
            }
        }
        public IObservable<bool> clearEvent {
            get {
                return clear;
            }
        }
        FadeSceneChanger sceneChanger;

        private void Start() {
            setup();
            Debug.Log("GameController Start");
            sceneChanger = new FadeSceneChanger(gameObject);
            GameTimeStopGraphicController gameTimeStopGraphicController = GetComponent<GameTimeStopGraphicController>();
            gameTimeStopGraphicController.TimeStop(0.0f);
            timeStop.Subscribe(b =>
            {
                if (b) gameTimeStopGraphicController.TimeStop(1.0f);
                else gameTimeStopGraphicController.TimeStop(0.0f);
            });

            sceneChanger.sceneOpen();
            var events = GameObject.FindGameObjectsWithTag("EventController");
            foreach (var e in events) {
                var t = e.GetComponent<Event_Trigger>();
                if (t) t.Event.Subscribe(talk).AddTo(gameObject);
            }
            var gate = GameObject.FindGameObjectWithTag("Finish");

            gate.GetComponent<Gate>().Event.Where(b => b).First()
                .Subscribe(_ => StartCoroutine(GoalStage(gate)))
                .AddTo(gameObject);
            Observable.CombineLatest(talkEvent, clearEvent).Select(list => list.Any(b => b)).Subscribe(f =>
            {
                freezed.Value = f;
            }).AddTo(gameObject);
        }

        IEnumerator GoalStage(GameObject gate) {
            PlayerData.ClearStage();

            AsyncOperation op;
            op = SceneManager.LoadSceneAsync(0);
            op.allowSceneActivation = false;
            clear.OnNext(true);
            yield return gate.GetComponent<Gate>().Event.Where(c => !c).First().ToYieldInstruction();
            yield return GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeTransition>().startTransition().ToYieldInstruction();
            op.allowSceneActivation = true;
            yield return null;
        }

        //時間停止
        public void try_timeStop() {
            if (timeStop.Value) return;
            FreezableFixedUpdate.First().Subscribe(_ => timeStop.OnNext(true));
            FreezableFixedUpdate.Skip(250).First().Subscribe(_ => timeStop.OnNext(false));
        }
    }
}