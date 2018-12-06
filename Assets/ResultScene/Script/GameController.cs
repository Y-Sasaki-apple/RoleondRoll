using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

namespace ResultScene {
    public class GameController : MonoBehaviour {
        FadeSceneChanger sceneChanger;
        void Start() {
            sceneChanger = new FadeSceneChanger(gameObject);
            sceneChanger.sceneOpen();
            this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Fire1"))
            .Subscribe(p => sceneChanger.sceneClose());
        }
    }
}