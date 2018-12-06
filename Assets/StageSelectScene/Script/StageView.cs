using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageView : MonoBehaviour {
    [SerializeField] GameObject StageBack1;
    [SerializeField] List<GameObject> Stage1;

    void Start() {

    }

    public void showStageBack(int stageNumber) {
        switch (stageNumber) {
        case 1:
            Instantiate(StageBack1);
            break;
        }
    }

    public void showStage(int stageNumber, int number) {
        switch (stageNumber) {
        case 1:
            Instantiate(Stage1[number - 1]);
            break;
        }
    }

    public void showNewStage(int stageNumber, int number) {
        GameObject gimic = null;
        switch (stageNumber) {
        case 1:
            gimic = Instantiate(Stage1[number - 1]);
            break;
        }
        if (gimic)
            gimic.GetComponent<Animator>().SetTrigger("show");
    }

    public Vector3 getStagePos(int stageNumber,int number) {
        switch (stageNumber) {
        case 1:
            return Stage1[number - 1].transform.position;
        }
        throw new System.Exception();
    }
}
