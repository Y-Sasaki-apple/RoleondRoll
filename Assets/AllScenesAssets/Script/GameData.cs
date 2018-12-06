using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class GameData {
    public enum SceneNumber {
        SS1 = 0,
        E1_1B1 = 3,
        E1_1B2 = 4,
        S1_1 = 2,
        RESULT = 1
    }

    public static readonly Dictionary<int, int> NextScene = new Dictionary<int, int> {
        {(int)SceneNumber.E1_1B1,(int)SceneNumber.S1_1 },//Debugging---------------------------------
        {(int)SceneNumber.E1_1B2,(int)SceneNumber.S1_1 },
        {(int)SceneNumber.RESULT,(int)SceneNumber.SS1 }
    };

    public static readonly Dictionary<StageID, int> stageToscene = new Dictionary<StageID, int> {
        {new StageID{stage=1,number=1},(int)SceneNumber.S1_1 },
    };

    public enum flagsID {
        Stage1_Event
    }

    public static readonly Dictionary<int, int> StageNumber = new Dictionary<int, int> {
        {1,3 }
    };
    public struct StageID {
        public int stage;
        public int number;
        public static StageID None = new StageID { stage = 0, number = 0 };
    }


    public static StageID NextStage(StageID cleared) {
        if (cleared.stage == 1 && cleared.number == 1)
            return new StageID { stage = 1, number = 2 };
        return StageID.None;
    }

    public enum Way { up = 0, right = 1, down = 2, left = 3 }
    public static StageID StageMove(StageID now, Way way) {
        if (now.stage == 1 && now.number == 1) {
            if ((way == Way.down) || (way == Way.right)) {
                return new StageID { stage = 1, number = 2 };
            }
        } else if (now.stage == 1 && now.number == 2) {
            if ((way == Way.up) || (way == Way.left)) {
                return new StageID { stage = 1, number = 1 };
            }
        }
        return StageID.None;
    }
}

