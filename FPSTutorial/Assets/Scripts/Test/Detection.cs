using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Detection : MonoBehaviour {

        private RaycastHit hit;
        [SerializeField] private LayerMask detectionLayer;
        private float checkRate = 0.5f;
        private float nextCheck;
        private Transform myTransform;
        private float range = 5;

        // Use this for initialization
        void Start() {
            SetInitial();
        }

        // Update is called once per frame
        void Update() {
            DetectItems();
        }

        void SetInitial() {
            myTransform = transform;
            detectionLayer = 1 << 9;
        }

        void DetectItems() {
            if(Time.time > nextCheck) {
                nextCheck = Time.time + checkRate;
                if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, range, detectionLayer)) {
                    Debug.Log(hit.transform.name + " is an item");
                }
            }
        }
    }
}

