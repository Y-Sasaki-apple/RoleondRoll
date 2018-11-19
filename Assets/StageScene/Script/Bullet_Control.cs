using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Control : MonoBehaviour {
    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        var vel = rb2d.velocity;
        vel.x = 5.0f;
        rb2d.velocity = vel;
    }
}
