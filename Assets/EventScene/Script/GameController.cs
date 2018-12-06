using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

namespace EventScene {
    public class GameController : MonoBehaviour {
        FadeSceneChanger sceneChanger;
        void Start() {
            var events = GameObject.FindGameObjectWithTag("EventController").GetComponent<Event_Master>();
            sceneChanger = new FadeSceneChanger(gameObject);
            sceneChanger.sceneOpen()
                .Subscribe(_ => { }, () =>
                    events.startEvent()
                    .Subscribe(_ => { }, () => sceneChanger.sceneClose()).AddTo(gameObject))
                .AddTo(gameObject);
        }
    }
}