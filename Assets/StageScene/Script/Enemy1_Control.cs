using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Control : MonoBehaviour {
    private Rigidbody2D rb2d;
    

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Bullet") {
            
        }
    }

    private void FixedUpdate() {
        var vel = rb2d.velocity;
        vel.x = -1.0f;
        rb2d.velocity = vel;
    }
}
