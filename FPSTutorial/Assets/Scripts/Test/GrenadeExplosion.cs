using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TBD {
    public class GrenadeExplosion : MonoBehaviour {

        private float destroyTime = 1;
        private Collider[] hitColliders;
        public float blastRadius;
        public float explosionPower;
        public LayerMask explosionLayers;

        void OnCollisionEnter(Collision col) {
            ExplosionWork(col.contacts[0].point);
            Destroy(gameObject);
        }

        void ExplosionWork(Vector3 explosionPoint) {
            hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);

            foreach(Collider hitCol in hitColliders) {

                if (hitCol.GetComponent<NavMeshAgent>() != null)
                    hitCol.GetComponent<NavMeshAgent>().enabled = false;

                if(hitCol.GetComponent<Rigidbody>() != null) {
                    hitCol.GetComponent<Rigidbody>().isKinematic = false;
                    hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
                }

                if(hitCol.CompareTag("Enemy")) {
                    Destroy(hitCol.gameObject, destroyTime);
                }
            }
        }
    }
}

