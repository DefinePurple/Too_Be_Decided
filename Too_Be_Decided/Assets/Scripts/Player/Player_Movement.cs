using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_Movement : MonoBehaviour {

        public float sensitivityX = 10f;
        public float sensitivityY = 10f;
        public float minY = -60f;
        public float maxY = 60f;
        float rotationY = 0f;

        public float movementSpeed = 5f;

        void Update() {
            float rotationX = Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            transform.GetChild(0).localEulerAngles = new Vector3(-rotationY, 0f, 0f);
            transform.Rotate(0f, rotationX, 0f);
        }

        void FixedUpdate() {
            float move = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        }
    }
}

