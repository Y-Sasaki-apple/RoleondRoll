using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

//public class GameEventController : MonoBehaviour {
//    private Subject<bool> timeStop = new Subject<bool>();
//    public IObservable<bool> TimeStopEvent {
//        get {
//            return timeStop;
//        }
//    }
//    public void try_timeStop() {

//    }

//    IEnumerator timeStopping() {
//        timeStop.OnNext(true);
//        yield return new WaitForSeconds(5.0f);
//        timeStop.OnNext(false);
//    }
//}
//    private bool timestopping = false;
//    private bool eventing = false;

//    public int frame_timestop=300;
//    private int count_timestop;
//    GameObject[] getallchildren() {
//        GameObject[] gameObjects = new GameObject[transform.childCount];
//        for (int i = 0; i < transform.childCount; i++) {
//            gameObjects[i] = transform.GetChild(i).gameObject;
//        }
//        return gameObjects;
//    }

//    public void startEvent() {
//        eventing = true;
//        var gameObjects = getallchildren();
//        foreach (var o in gameObjects) {
//            ExecuteEvents.Execute<IGameEventHandler>(
//                target: o,
//                eventData: null,
//                functor: (reciever, eventData) => reciever.eventStart()
//                );
//        }
//    }

//    public void endEvent() {
//        eventing = false;
//        var gameObjects = getallchildren();
//        foreach (var o in gameObjects) {
//            ExecuteEvents.Execute<IGameEventHandler>(
//                target: o,
//                eventData: null,
//                functor: (reciever, eventData) => reciever.eventEnd()
//                );
//        }
//    }
//    private void startTimeStop() {
//        timestopping = true;
//        var gameObjects = getallchildren();
//        foreach (var o in gameObjects) {
//            ExecuteEvents.Execute<ITimeStopHandler>(
//                target: o,
//                eventData: null,
//                functor: (reciever, eventData) => reciever.TimeStopStart()
//                );
//        }
//    }

//    private void endTimeStop() {
//        timestopping = false;
//        var gameObjects = getallchildren();
//        foreach (var o in gameObjects) {
//            ExecuteEvents.Execute<ITimeStopHandler>(
//                target: o,
//                eventData: null,
//                functor: (reciever, eventData) => reciever.TimeStopEnd()
//                );
//        }
//    }

//    public void timestop() {
//        StartCoroutine(timestopping_process());
//    }

//    IEnumerator timestopping_process() {
//        startTimeStop();
//        count_timestop = frame_timestop;
//        while (count_timestop > 0) {
//            if(!eventing)count_timestop--;
//            yield return null;
//        }
//        endTimeStop();
//    }
//}
