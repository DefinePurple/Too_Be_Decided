using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_StandardInput : MonoBehaviour {

        private Gun_Master gunMaster;
        private float nextAttack;
        public float attackRate = 0.3f;
        private Transform myTransform;
        public string attackButtonName;
        public string reloadButtonName;

        void Start() {
            SetInitial();
        }

        void Update() {
            CheckFire();
            CheckReload();
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            gunMaster.isGunLoaded = true;
        }


        void CheckFire() {
            if(Time.time > nextAttack) {
                if (Input.GetButton(attackButtonName)) {
                    AttemptFire();
                }
            }   
        }

        void AttemptFire() {
            nextAttack = Time.time + attackRate;
            if (gunMaster.isGunLoaded) {
                Debug.Log("Shooting");
                gunMaster.CallEventPlayerInput();
            } else {
                gunMaster.CallEventGunNotUsable();
            }
        }

        void CheckReload() {
            if (Input.GetButtonDown(reloadButtonName))
                gunMaster.CallEventReload();
        }


    }
}

