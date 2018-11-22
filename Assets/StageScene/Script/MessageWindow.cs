using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour {
    public int charframe;
    public GameObject windowBack;
    private Text text;

    private int charnum = 0;
    private int framecount = 0;

    private bool able_textshow = false;
    private string message = "";
    private Animator animator;

    // Use this for initialization
    void Start() {
        windowBack = gameObject.transform.Find("WindowBack").gameObject;
        text = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    public void Show() {
        windowBack.GetComponent<Animator>().SetBool("Show", true);
        StartCoroutine(textShow_process());
    }

    public void Hide() {
        windowBack.GetComponent<Animator>().SetBool("Show", false);
        able_textshow = false;
        text.text = "";
    }
    IEnumerator textShow_process() {
        able_textshow = false;
        yield return new WaitForSeconds(0.5f);
        able_textshow = true;
    }
    public void startShowText(string m) {
        charnum = 0;
        framecount = 0;
        message = m;
    }



    private void FixedUpdate() {
        if (able_textshow && charnum < message.Length) {
            framecount++;
            if (framecount % charframe == 0) {
                framecount = 0;
                charnum++;
                text.text = message.Substring(0, charnum);
            }
        }
    }

}
