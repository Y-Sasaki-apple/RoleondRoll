using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace StageScene {
    public class Event1_1_1 : Event_Trigger {
        public MessageWindow mw;
        public Alice_Control alice;
        protected override IEnumerator gameEvent() {
            alice = GameObject.FindGameObjectWithTag("Alice").GetComponent<Alice_Control>();
            mw = GameObject.FindGameObjectWithTag("MessageWindow").GetComponent<MessageWindow>();
            mw.show();
            TalkEvent.OnNext(true);
            yield return mw.next("Hello!!!").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();
            yield return mw.next("This is The first Event!").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();
            mw.hide();
            TalkEvent.OnNext(false);
            Destroy(gameObject);
            yield return null;
        }
    }
}