using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

namespace StageSelectScene {
    public class GameController : MonoBehaviour {
        FadeSceneChanger sceneChanger;
        StageView stageView;
        void Start() {
            GameObject alice = GameObject.FindGameObjectWithTag("Alice");
            stageView = GameObject.FindGameObjectWithTag("StageView").GetComponent<StageView>();

            //ステージの表示
            int stage = PlayerData.nowStage;
            stageView.showStageBack(stage);
            for (int i = 0; i < GameData.StageNumber[stage]; i++) {
                if (PlayerData.AppearStageFlag[new GameData.StageID { stage = stage, number = i + 1 }]) {
                    stageView.showStage(stage, i + 1);
                }
            }
            var nextStage = PlayerData.ApperNextStage();//----------------------------------------------------------
            if (!nextStage.Equals(GameData.StageID.None)) {
                stageView.showNewStage(nextStage.stage, nextStage.number);
            }

            //プレイヤーの移動
            this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Up")).Subscribe(_ =>
            {
                var newstage = GameData.StageMove(new GameData.StageID { stage = PlayerData.nowStage, number = PlayerData.nowNumber }, GameData.Way.up);
                if (newstage.Equals(GameData.StageID.None) || !PlayerData.AppearStageFlag[newstage]) return;
                PlayerData.nowStage = newstage.stage;
                PlayerData.nowNumber = newstage.number;
                alice.transform.position = stageView.getStagePos(PlayerData.nowStage, PlayerData.nowNumber);
            });
            this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Down")).Subscribe(_ =>
            {
                var newstage = GameData.StageMove(new GameData.StageID { stage = PlayerData.nowStage, number = PlayerData.nowNumber }, GameData.Way.down);
                if (newstage.Equals(GameData.StageID.None) || !PlayerData.AppearStageFlag[newstage]) return;
                PlayerData.nowStage = newstage.stage;
                PlayerData.nowNumber = newstage.number;
                alice.transform.position = stageView.getStagePos(PlayerData.nowStage, PlayerData.nowNumber);
            });
            this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Left")).Subscribe(_ =>
            {
                var newstage = GameData.StageMove(new GameData.StageID { stage = PlayerData.nowStage, number = PlayerData.nowNumber }, GameData.Way.left);
                if (newstage.Equals(GameData.StageID.None) || !PlayerData.AppearStageFlag[newstage]) return;
                PlayerData.nowStage = newstage.stage;
                PlayerData.nowNumber = newstage.number;
                alice.transform.position = stageView.getStagePos(PlayerData.nowStage, PlayerData.nowNumber);
            });
            this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Right")).Subscribe(_ =>
            {
                var newstage = GameData.StageMove(new GameData.StageID { stage = PlayerData.nowStage, number = PlayerData.nowNumber }, GameData.Way.right);
                if (newstage.Equals(GameData.StageID.None) || !PlayerData.AppearStageFlag[newstage]) return;
                PlayerData.nowStage = newstage.stage;
                PlayerData.nowNumber = newstage.number;
                alice.transform.position = stageView.getStagePos(PlayerData.nowStage, PlayerData.nowNumber);
            });


            sceneChanger = new FadeSceneChanger(gameObject);
            sceneChanger.sceneOpen();
            this.UpdateAsObservable().Where(_ => Input.GetButtonDown("Fire1")).Take(1).Subscribe(_ =>
                sceneChanger.sceneClose(PlayerData.GoScene(new GameData.StageID { stage = PlayerData.nowStage, number = PlayerData.nowNumber }))
            );
        }
    }
}