using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_StandardInput : MonoBehaviour {

        private Gun_Master gunMaster;
        private Player_Master playerMaster;
        private GameManager_Master gameManagerMaster;
        private float nextAttack;
        public float attackRate = 0.3f;
        //private Transform myTransform;
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
            playerMaster = GetComponentInParent<Player_Master>();
            gameManagerMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager_Master>();
           // myTransform = transform;
            gunMaster.isGunLoaded = true;
        }


        void CheckFire() {
            if(Time.time > nextAttack && playerMaster.IsPlayer()) {
                if (Input.GetButton(attackButtonName) && !gameManagerMaster.isMenuOn) {
                    AttemptFire();
                }
            }   
        }

        void AttemptFire() {
            nextAttack = Time.time + attackRate;
            if (gunMaster.isGunLoaded) {
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

