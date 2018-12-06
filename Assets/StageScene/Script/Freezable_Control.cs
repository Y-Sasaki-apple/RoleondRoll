using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace StageScene {
    public class Freezable_Control : MonoBehaviour {
        public BoolReactiveProperty freezed = new BoolReactiveProperty(false);
        public IObservable<Unit> FreezableUpdate;
        public IObservable<Unit> FreezableFixedUpdate;
        protected void setup() {
            FreezableUpdate = this.UpdateAsObservable().Where(_ => !freezed.Value).Publish().RefCount();
            FreezableFixedUpdate = this.FixedUpdateAsObservable().Where(_ => !freezed.Value).Publish().RefCount();
        }
    }

}