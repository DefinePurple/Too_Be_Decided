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

        public List<AmmoTypes> typesOfAmmunition = new List<AmmoTypes>();

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

        void PickedUpAmmo(string ammoName, int quantity) {
            for(int i = 0; i < typesOfAmmunition.Count; i++) {
                if(typesOfAmmunition[i].ammoName == ammoName) {
                    typesOfAmmunition[i].ammoCurrentCarried += quantity;
                    
                    if(typesOfAmmunition[i].ammoCurrentCarried > typesOfAmmunition[i].maxQuantity)
                        typesOfAmmunition[i].ammoCurrentCarried = typesOfAmmunition[i].maxQuantity;

                    playerMaster.CallEventAmmoChanged();
                    break;
                }
            }
        }
	}
}

