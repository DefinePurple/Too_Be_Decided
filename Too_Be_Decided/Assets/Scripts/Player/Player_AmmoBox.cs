using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_AmmoBox : MonoBehaviour {

        private Player_Master playerMaster;
        public int ammoCurrentCarried;//Amount of ammo currently on the player not in the clip
        public int maxQuantity;//max amount for previous

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

        //When pickup ammo, add it to the current amount carried
        void PickedUpAmmo(int quantity) {
            ammoCurrentCarried += quantity;

            if(ammoCurrentCarried > maxQuantity)
                ammoCurrentCarried = maxQuantity;

            playerMaster.CallEventAmmoChanged();
        }
	}
}