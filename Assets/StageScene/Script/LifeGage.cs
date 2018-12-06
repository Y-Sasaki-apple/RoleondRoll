using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
namespace StageScene {
    public class LifeGage : MonoBehaviour {
        private List<GameObject> LifeMarks = new List<GameObject>();
        public GameObject LifeMark;
        public GameController game;

        void Start() {
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            var LifeMax = game.life.Value;
            for (int i = 0; i < LifeMax; i++) {
                var lm = Instantiate(LifeMark);
                lm.transform.position = new Vector3(i * 20, 0);
                lm.transform.SetParent(gameObject.transform);
                LifeMarks.Add(lm);
            }

            game.life.Subscribe(life => {
                for (int i = 0; i < LifeMax; i++) {
                    LifeMarks[i].GetComponent<Animator>().SetBool("Enable", i < life);
                }
            }).AddTo(gameObject);
        }

        // Update is called once per frame
        void Update() {

        }
    }
}