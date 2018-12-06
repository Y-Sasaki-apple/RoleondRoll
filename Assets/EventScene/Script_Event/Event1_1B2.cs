using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace EventScene {
    public class Event1_1B2 : Event_Master {
        [SerializeField] GameObject Chara1;
        [SerializeField] GameObject Chara2;
        protected override IEnumerator gameEvent(Subject<Unit> s) {
            MessageWindow mw = GameObject.Find("MessageWindow").GetComponent<MessageWindow>();
            Charactor_Controller alice = Instantiate(Chara1).GetComponent<Charactor_Controller>();
            Charactor_Controller hatter = Instantiate(Chara2).GetComponent<Charactor_Controller>();
            yield return null;
            
            alice.show();
            mw.show();

            alice.next(5);
            yield return mw.next("「あれ、ここはどこ？\nたしか、お父さまの書斎で絵本を見つけて……」").ToYieldInstruction();
            alice.next(6);
            yield return waitForFire.ToYieldInstruction();

            hatter.next(2);
            hatter.show();
            yield return mw.next("「お目覚めかな？」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            alice.next(7);
            yield return mw.next("「（……！？）」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();
            yield return mw.next("「ゴメンゴメン、驚かせちゃったネ。\n僕は帽子屋（ハッター）。怪しい者じゃないよ？」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();
            yield return hatter.hide().ToYieldInstruction();
            hatter.next(1);
            hatter.show();
            alice.next(8);
            yield return mw.next("「ここはどこなのかしら？」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            yield return mw.next("「よくぞ訊いてくれた！").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();
            yield return mw.next("ここは誰もが覚えていて、誰一人として知らない\nどこでもあって、どこでもない場所").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();
            yield return mw.next("あるべきモノが無くて、ないはずのモノが存る\n不思議なセカイさ！」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            alice.next(8);
            yield return mw.next("「（……、胡散臭い）」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            hatter.next(3);
            yield return mw.next("「あーその顔は信じてないなーキミ？\nダメだよ～夢を見るのが子供の仕事なんだから").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            yield return mw.next("そして夢を見せるのは大人のシ・ゴ・ト」").ToYieldInstruction();
            yield return waitForFire.ToYieldInstruction();

            //演出----------------------------------------------------------------------------------------------
            alice.next(2);
            yield return mw.next("「すごーい！　今のどうやったの！？\nそれにこの服とっても可愛らしいわ」").ToYieldInstruction();
            alice.next(9);
            hatter.next(4);
            yield return mw.next("「気に入ってくれたみたいだね\n喜んでもらえたなら僕も嬉しいよ。").ToYieldInstruction();
            hatter.next(5);
            yield return mw.next("その服の代わりと言ってはなんだけど、君の名前を教えてもらえるかな？").ToYieldInstruction();
            //Selection



            alice.hide();

            mw.hide();

            s.OnCompleted();
            yield return null;
        }
    }
}