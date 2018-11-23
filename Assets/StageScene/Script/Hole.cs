using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hole : MonoBehaviour {
    public Transform returnPoint;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Alice") {
            StartCoroutine(returnposition(collision.gameObject));
        }
    }
    private IEnumerator returnposition(GameObject alice) {
        yield return new WaitForSeconds(1.0f);
        alice.GetComponent<Rigidbody2D>().velocity = new Vector2();
        alice.transform.position = returnPoint.position;
        alice.GetComponent<Alice_Control>().Damaged();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
