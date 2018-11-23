using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicThings_Control : MonoBehaviour {
    protected Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            animator.SetTrigger("magiced");
            tag = "Ground";
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
