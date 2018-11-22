using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Event_Trigger : MonoBehaviour {
    public MessageWindow mw;
    private bool event_started = false;
    public Alice_Control alice;
    void Start() {
        mw = GameObject.FindGameObjectWithTag("MessageWindow").GetComponent<MessageWindow>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Alice") {
            event_started = true;
            alice = collider.gameObject.GetComponent<Alice_Control>();
            StartCoroutine(gameEventProcess());
        }
    }

    private void Update() {

    }

    IEnumerator gameEventProcess() {
        foreach (var e in gameEvent()) {
            yield return e;
            while (!Input.GetButtonDown("Fire1")) {
                yield return null;
            }
        }
    }

    IEnumerable<int> gameEvent() {
        mw.Show();
        alice.startEvent();
        mw.startShowText("Do you have any questions?\nなにか質問とかある？");
        yield return 0;
        mw.startShowText("テストメッセージですけど。何か？");
        yield return 1;
        mw.Hide();
        Destroy(gameObject);
        alice.finishEvent();
        yield return 2;
    }
}
