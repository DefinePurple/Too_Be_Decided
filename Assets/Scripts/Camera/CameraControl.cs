using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float minY = -60f;
    public float maxY = 60f;
    float rotationY = 0f;

    // Use this for initialization
    void Start () {
		
	}

    void Update() {
        float rotationX = Input.GetAxis("Mouse X") * sensitivityX;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        transform.localEulerAngles = new Vector3(-rotationY, 0f, 0f);
        transform.parent.Rotate(0f, rotationX, 0f);
    }
}
