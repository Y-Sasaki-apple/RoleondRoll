using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneChanger {
    GameObject game;
    public FadeSceneChanger(GameObject gameObject) {
        game = gameObject;
    }
    public IObservable<Unit> sceneOpen() {
        var fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeTransition>();
        return fade.openTransition();
    }

    public void sceneClose() {
        this.sceneClose(GameData.NextScene[SceneManager.GetActiveScene().buildIndex]);
    }
    public void sceneClose(int SceneID) {
        AsyncOperation op;
        op = SceneManager.LoadSceneAsync(SceneID);
        op.allowSceneActivation = false;
        GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeTransition>()
        .startTransition().Subscribe(___ => { }, () =>
        {
            op.allowSceneActivation = true;
        });
    }
}
