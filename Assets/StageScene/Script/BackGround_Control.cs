using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BackGround_Control : MonoBehaviour {
    [SerializeField] List<Material> layers;
    [SerializeField] List<float> sppeds;
    [SerializeField] GameObject cam;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        var x = cam.transform.position.x;
        var y = cam.transform.position.y;
        for (int i = 0; i < layers.Count; i++) {
            layers[i].SetTextureOffset("_MainTex", new Vector2(x * sppeds[i], 0));
        }
    }
}
