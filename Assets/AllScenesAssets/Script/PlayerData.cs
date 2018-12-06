using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class PlayerData {
    public static Dictionary<GameData.flagsID, bool> BoolFlags = new Dictionary<GameData.flagsID, bool> {
        {GameData.flagsID.Stage1_Event,false }
    };
    public static int nowStage = 1;
    public static int nowNumber = 1;

    public static int prevClearStage = 0;
    public static int prevClearNumber = 0;

    public static GameData.StageID ClearStage() {
        prevClearStage = nowStage;
        prevClearNumber = nowNumber;
        ClearStageFlag[new GameData.StageID { stage = nowStage, number = nowNumber }] = true;
        return new GameData.StageID { stage = nowStage, number = nowNumber };
    }

    public static GameData.StageID ApperNextStage() {
        var id = GameData.NextStage(new GameData.StageID { stage = prevClearStage, number = prevClearNumber });
        if (id.Equals(GameData.StageID.None)) return GameData.StageID.None;
        if (!AppearStageFlag[id]) {
            AppearStageFlag[id] = true;
            return id;
        } else {
            return GameData.StageID.None;
        }
    }

    public static Dictionary<GameData.StageID, bool> ClearStageFlag = new Dictionary<GameData.StageID, bool> {
        {new GameData.StageID{stage = 1,number =1},false },
        {new GameData.StageID{stage = 1,number =2},false },
        {new GameData.StageID{stage = 1,number =3},false },
    };

    public static Dictionary<GameData.StageID, bool> AppearStageFlag = new Dictionary<GameData.StageID, bool> {
        {new GameData.StageID{stage = 1,number =1},true },
        {new GameData.StageID{stage = 1,number =2},false },
        {new GameData.StageID{stage = 1,number =3},false },
    };

    public static int GoScene(GameData.StageID stage_wannago) {
        if (stage_wannago.stage == 1 && stage_wannago.number == 1) {
            if (!BoolFlags[GameData.flagsID.Stage1_Event]) {
                BoolFlags[GameData.flagsID.Stage1_Event] = true;
                return (int)GameData.SceneNumber.E1_1B1;
            } else {
                return (int)GameData.SceneNumber.S1_1;
            }
        }
        throw new Exception();
    }
}
