using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace EventScene {
    public class Event1_1B1 : Event_Master {
        [SerializeField] GameObject Chara1;
        protected override IEnumerator gameEvent(Subject<Unit> s) {
            MessageWindow mw = GameObject.Find("MessageWindow").GetComponent<MessageWindow>();
            //Charactor_Controller alice = GameObject.Find("Alice").GetComponent<Charactor_Controller>();
            //Charactor_Controller alice = Instantiate(Chara1).GetComponent<Charactor_Controller>();
            Effect_Controller white = GameObject.Find("WhiteOuter").GetComponent<Effect_Controller>();
            yield return null;

            //alice.show();
            mw.show();

            //alice.next(2);
            yield return mw.next("「本がたくさん！\nでも、どれも難しくて読めそうにないわ……」").ToYieldInstruction();
            //alice.next(3);
            yield return waitForFire.ToYieldInstruction();

            yield return mw.next("少女が何か面白いものはないのかと探していると、\nタイトルの無い古ぼけた本が一冊、少女の目に入った。").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            //alice.next(4);
            yield return mw.next("「（何かしらこの本は？）」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();


            yield return mw.next("少女が本を手に取りパラパラとページをめくると\n多くの挿絵からなる絵本のようであった。").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            //alice.next(1);
            yield return mw.next("「あら、お父さまも絵本を読むのね。\nでもこの本、途中から白紙だわ——」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            yield return mw.next("本が眩い光を放ち、少女を包み込む。").ToYieldInstruction();
            //alice.hide();
            yield return white.show().ToYieldInstruction();

            mw.hide();

            s.OnCompleted();
            yield return null;
        }
    }
}