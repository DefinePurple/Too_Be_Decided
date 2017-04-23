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
            gunMaster.isGunLoaded = true;
        }


        void CheckFire() {
            //Check if the time has passed to allow firing
            //and checks if the one firing is the player
            if(Time.time > nextAttack && playerMaster.IsPlayer()) {
                //checks if button is pressed and the menu is not on
                if (Input.GetAxis(attackButtonName) > 0 && !gameManagerMaster.isMenuOn) {
                    AttemptFire();
                }
            }   
        }

        void AttemptFire() {
            //sets the time to next attack
            nextAttack = Time.time + attackRate;
            //if the gun is loaded fire
            if (gunMaster.isGunLoaded) {
                gunMaster.CallEventPlayerInput();
            } else {//otherwise the gun is not usable
                gunMaster.CallEventGunNotUsable();
            }
        }

        void CheckReload() {
            //check if player wants to reload
            if (Input.GetButtonDown(reloadButtonName))
                gunMaster.CallEventReload();
        }
    }
}

