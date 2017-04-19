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
            gunMaster.EventShotEnemy += Test;
        }

        void OnDisable() {
            gunMaster.EventPlayerInput -= OpenFire;
            gunMaster.EventShotEnemy -= Test;
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            camTransform = myTransform.parent;
        }

        void OpenFire() {
            Debug.Log("Open Fire Called");
            if(Physics.Raycast(camTransform.position, camTransform.forward, out hit, range)) {
                gunMaster.CallEventShotDefault(hit.transform);
                if(hit.transform.CompareTag(GameManager_References._enemyTag)) {
                    Debug.Log("Shot Enemy");
                    gunMaster.CallEventShotEnemy(hit.transform);
                }
            }
        }

        void Test(Transform hitTransform) {
            Debug.Log("Test");
        }
    }
}

