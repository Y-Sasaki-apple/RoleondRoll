using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StageScene {
    class GameTimeStopGraphicController : MonoBehaviour{
        [SerializeField] Material timestopped;

        public void TimeStop(float f) {
            timestopped.SetFloat("TimeStop", f);
        }
    }
}
