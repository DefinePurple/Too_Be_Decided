using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_HitAnimation : MonoBehaviour {

        private Gun_Master gunMaster;
        public GameObject prefab;

        void OnEnable() {
            SetInitial();
            gunMaster.EventShotDefault += HitAnimation;
        }

        void OnDisable() {
            gunMaster.EventShotDefault -= HitAnimation;
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
        }

        void HitAnimation(Transform hit) {
            Instantiate(prefab, hit.position, Quaternion.identity);
        }
    }
}