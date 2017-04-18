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
        private float offsetFactor = 7;
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
            Debug.Log("Open Fire Called");
            if(Physics.Raycast(camTransform.position, camTransform.forward, out hit, range)) {
                gunMaster.CallEventShotDefault(hit.point, hit.transform);
                if(hit.transform.CompareTag(GameManager_References._enemyTag)) {
                    Debug.Log("Shot Enemy");
                    gunMaster.CallEventShotEnemy(hit.point, hit.transform);
                }
            }
        }
    }
}

