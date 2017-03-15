using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Gun_StandardInput : MonoBehaviour {

        private Gun_Master gunMaster;
        private float nextAttack;
        public float attackRate = 0.8f;
        private Transform myTransform;
        public bool isAutomatic;
        public string attackButtonName;
        public string reloadButtonName;
		

        void OnEnable() {
            SetInitial();
        }

		// Update is called once per frame
		void Update () {
            CheckIfWeaponCanAttack();
            CheckForReloadRequest();
		}

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            gunMaster.isGunLoaded = true;
        }

        void CheckIfWeaponCanAttack() {
            if(Time.time > nextAttack && Time.timeScale > 0) {
                if (Input.GetAxis(attackButtonName) != 0 && isAutomatic) {
                    Debug.Log("Full Auto");
                    AttemptAttack();
                } else if(Input.GetButtonDown(attackButtonName) && !isAutomatic) {
                    Debug.Log("Pistol");
                    AttemptAttack();
                }
            }
        }

        void AttemptAttack() {
            nextAttack = Time.time + attackRate;

            if (gunMaster.isGunLoaded) {
                gunMaster.CallEventPlayerInput();
            } else {
                gunMaster.CallEventGunNotUsable();
            }

        }

        void CheckForReloadRequest() {
            if(Input.GetButtonDown(reloadButtonName) && Time.timeScale > 0) {
                Debug.Log("Reload");
                gunMaster.CallEventRequestReload();
            }
        }
	}
}

