using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TBD {
    public class Shoot : MonoBehaviour {

        private float fireRate = 0.3f;
        private float nextFire;
        private RaycastHit hit;
        private float range = 300f;
        private Transform myTransform;

        // Use this for initialization
        void Start() {
            SetInitial();
        }

        // Update is called once per frame
        void Update() {
            CheckForInput();
        }

        void SetInitial() {
            myTransform = transform;
        }

        void CheckForInput() {
            /*if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire){
                nextFire = Time.time + fireRate;

                Debug.Log("Mouse Pressed");
            }*/

            if (Input.GetButton("Fire1") && Time.time > nextFire) {
                Debug.DrawRay(myTransform.TransformPoint(0 , 0, 1), transform.forward, Color.green, 3);
                if(Physics.Raycast(myTransform.TransformPoint(0, 0, 1), transform.forward, out hit, range)) {
                    if (hit.transform.CompareTag("Enemy"))
                        Debug.Log("Enemy: " + hit.transform.name);
                    else Debug.Log("Not an enemy");
                }
                nextFire = Time.time + fireRate;

                Debug.Log("Mouse Pressed");
            }

        }
    }
}
