using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_AmmoBox : MonoBehaviour {

        private Player_Master playerMaster;

        [System.Serializable]
        public class AmmoTypes {
            public string ammoName;
            public int ammoCurrentCarried;
            public int maxQuantity;

            public AmmoTypes(string aName, int aCurrentCarried, int aMaxQuantity) {
                ammoName = aName;
                ammoCurrentCarried = aCurrentCarried;
                maxQuantity = aMaxQuantity;
            }
        }

        public AmmoTypes ammunition;

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
            ammunition.ammoCurrentCarried += quantity;
                    
            if(ammunition.ammoCurrentCarried > ammunition.maxQuantity)
                ammunition.ammoCurrentCarried = ammunition.maxQuantity;

            playerMaster.CallEventAmmoChanged();
        }
	}
}

