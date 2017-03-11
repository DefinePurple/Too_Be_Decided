using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TBD {
    public class EnemyChase : MonoBehaviour {

        public LayerMask detectionLayer;
        private Transform myTransform;
        private NavMeshAgent myNavMeshAgent;
        private Collider[] hitColliders;
        private float checkRate;
        private float nextCheck;
        private float detectionRadius;

        // Use this for initialization
        void Start() {
            SetInitial();
        }

        // Update is called once per frame
        void Update() {
            CheckIfPlayerInRange();
        }

        void SetInitial() {
            myTransform = transform;
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            checkRate = Random.Range(0.8f, 1.2f);
            detectionRadius = 10;
        }

        void CheckIfPlayerInRange() {
            if(Time.time > nextCheck) {
                nextCheck = Time.time + checkRate;

                hitColliders = Physics.OverlapSphere(myTransform.position, detectionRadius, detectionLayer);

                if(hitColliders.Length > 0) {
                    myNavMeshAgent.SetDestination(hitColliders[0].transform.position);
                }
            }
        }
    }
}

