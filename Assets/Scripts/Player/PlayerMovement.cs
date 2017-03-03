using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 5;

    void Start () {
	}

	void FixedUpdate () {
        float forward = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float side = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(side, 0, forward);
	}
}
