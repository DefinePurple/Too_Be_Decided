using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletMovement : MonoBehaviour {

    public float movementSpeed = 5;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider col) {
        Destroy(gameObject);
    }
}
