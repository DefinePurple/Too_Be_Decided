using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_AmmoBox : MonoBehaviour {

        private Player_Master playerMaster;
        public int ammoCurrentCarried;
        public int maxQuantity;

        void OnEnable() {
            SetInitial();
            playerMaster.EventPickedUpAmmo += PickedUpAmmo;
        }

        void OnDisable() {
            playerMaster.EventPickedUpAmmo -= PickedUpAmmo;
        }

        void SetInitial() {
            playerMaster = GetComponent<Player_Master>();
        }

        void PickedUpAmmo(int quantity) {
            ammoCurrentCarried += quantity;
                    
            if(ammoCurrentCarried > maxQuantity)
                ammoCurrentCarried = maxQuantity;

            playerMaster.CallEventAmmoChanged();
        }
	}
}

