using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Enemy_Detection : MonoBehaviour {

        private Enemy_Master enemyMaster;
        private Transform myTransform;
        public Transform head;
        public LayerMask playerLayer;
        public LayerMask sightLayer;
        private float checkRate;
        private float nextCheck;
        private float detectionRadius = 80f;
        private RaycastHit hit; 
		
        void OnEnable() {
            SetInitial();
            enemyMaster.EventEnemyDie += DisableThis;
        }

        void OnDisable() {
            enemyMaster.EventEnemyDie -= DisableThis;
        }

		// Update is called once per frame
		void Update () {
            CarryOutDetection();
		}

        void SetInitial() {
            enemyMaster = GetComponent<Enemy_Master>();
            myTransform = transform;

            if(head == null) {
                head = myTransform;
            }

            checkRate = Random.Range(0.8f, 1.2f);
        }

        void CarryOutDetection() {
            if(Time.time > nextCheck) {
                nextCheck = Time.time + nextCheck;
                Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectionRadius, playerLayer);
                if(colliders.Length > 0) {
                    foreach(Collider col in colliders) {
                        if (col.CompareTag(GameManager_References._playerTag)) {
                            if (CanTargetBeSeen(col.transform)) {
                                break;
                            }
                        }
                    }   
                } else {
                    enemyMaster.CallEventEnemyLostTarget();
                }
            }
        }

        void DisableThis() {
            this.enabled = false;
        }
        
        bool CanTargetBeSeen(Transform potentialTarget) {
            if (Physics.Linecast(head.position, potentialTarget.position, out hit, sightLayer)) {
                if (hit.transform == potentialTarget) {
                    enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
                    return true;
                } else {
                    enemyMaster.CallEventEnemyLostTarget();
                    return false;
                }
            } else {
                enemyMaster.CallEventEnemyLostTarget();
                return false;
            }
        }
	}
}

