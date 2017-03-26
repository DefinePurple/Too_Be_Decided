using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Gun_Shoot : MonoBehaviour {

        private Gun_Master gunMaster;
        private Transform myTransform;
        private Transform camTransform;
        private RaycastHit hit;
        public float range = 400f;
        private float offsetFactor = 7;
        private Vector3 startPosition;

        void OnEnable() {
            SetInitial();
            gunMaster.EventPlayerInput += OpenFire;
            gunMaster.EventSpeedCaptured += StartShootingPosition;
        }

        void OnDisable() {
            gunMaster.EventPlayerInput -= OpenFire;
            gunMaster.EventSpeedCaptured -= StartShootingPosition;
        }

		void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            camTransform = transform.parent;

        }

        void OpenFire() {
            Debug.Log("OpenFire Called");
            if(Physics.Raycast(camTransform.TransformPoint(startPosition), camTransform.forward, out hit, range)) {
                gunMaster.CallEventShotDefault(hit.point, hit.transform);

                if (hit.transform.CompareTag(GameManager_References._enemyTag)) {
                    Debug.Log("Shot Enemy");
                    gunMaster.CallEventShotEnemy(hit.point, hit.transform);
                }
            }
        }

        void StartShootingPosition(float playerSpeed) {
            float offset = playerSpeed / offsetFactor;

            startPosition = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 1);
        }
	}
}

