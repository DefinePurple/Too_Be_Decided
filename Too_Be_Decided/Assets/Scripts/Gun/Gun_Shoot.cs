using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_Shoot : MonoBehaviour {

        private Gun_Master gunMaster;
        private Transform myTransform;
        private Transform camTransform;
        private RaycastHit hit;
        public float range = 400;
        private Vector3 StartPosition;

        void OnEnable() {
            SetInitial();
            gunMaster.EventPlayerInput += OpenFire;
        }

        void OnDisable() {
            gunMaster.EventPlayerInput -= OpenFire;
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            camTransform = myTransform.parent;
        }

        void OpenFire() {
            //if the raycast hits something
            if(Physics.Raycast(camTransform.position, camTransform.forward, out hit, range)) {
                gunMaster.CallEventShotDefault(hit.transform);//call the default event
                //if what is hit is the enemy
                if(hit.transform.CompareTag(GameManager_References._enemyTag)) {
                    gunMaster.CallEventShotEnemy(hit.transform);//call the enemy event
                }
            }
        }
    }
}

