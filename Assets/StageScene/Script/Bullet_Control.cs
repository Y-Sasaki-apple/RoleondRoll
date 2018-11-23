using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Control : MonoBehaviour {
    private Rigidbody2D rb2d;
    public Vector3 velocity;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = velocity;
    }
	
	void Update () {

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Gimic")
            Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Ground")
            Destroy(gameObject);
    }
    private void FixedUpdate() {
    }
}
