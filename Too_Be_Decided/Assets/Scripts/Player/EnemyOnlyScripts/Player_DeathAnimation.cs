using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Player_DeathAnimation : MonoBehaviour {

        private Player_Master playerMaster;
        public GameObject prefab;
        public LayerMask layerMask;

        void OnEnable() {
            SetInitial();
            playerMaster.EventDeathAnimation += DeathAnimation;
        }

        void OnDisable() {
            playerMaster.EventDeathAnimation -= DeathAnimation;
        }

        void SetInitial() {
            playerMaster = GetComponent<Player_Master>();
        }

        void DeathAnimation() {
            float radius = 2f;
            float power = 150.0f;
            for(int i = 0; i < 10; i++) {
                float x = transform.position.x + Random.Range(-0.5f, 0.5f);
                float y = transform.position.y + Random.Range(0f, 1f);
                float z = transform.position.z + Random.Range(-0.5f, 0.5f);
                Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity);
            }

            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, layerMask);
            foreach (Collider hit in colliders) {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3f);
            }
        }
    }
}