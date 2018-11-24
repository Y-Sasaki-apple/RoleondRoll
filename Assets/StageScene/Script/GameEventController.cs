using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEventController : Freezable_Control {
    private BehaviorSubject<bool> timeStop = new BehaviorSubject<bool>(false);
    private BehaviorSubject<bool> talk = new BehaviorSubject<bool>(false);

    private void Start() {
        setup();
        talkEvent.Subscribe(f => {
            freezed.Value = f;
        });
    }
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
    public void try_timeStop() {
        FreezableFixedUpdate.First().Subscribe(_ => timeStop.OnNext(true));
        FreezableFixedUpdate.Skip(250).First().Subscribe(_ => timeStop.OnNext(false));
    }

    public void startTalk() {
        talk.OnNext(true);
    }
    public void endTalk() {
        talk.OnNext(false);
    }
}
