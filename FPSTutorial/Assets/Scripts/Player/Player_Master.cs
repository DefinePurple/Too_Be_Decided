using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_Master : MonoBehaviour {

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventInventoryChanged;
        public event GeneralEventHandler EventHandsEmpty;
        public event GeneralEventHandler EventAmmoChanged;

        public delegate void AmmoPickupEventHandler(string ammoName, int quantity);
        public event AmmoPickupEventHandler EventPickedUpAmmo;

        public delegate void PlayerHealthEventHandler(int healthChange);
        public event PlayerHealthEventHandler EventPlayerHealthReduce;
        public event PlayerHealthEventHandler EventPlayerHealthIncrease;

        public void CallEventInventoryChanged() {
            if(EventInventoryChanged != null) {
                EventInventoryChanged();
            }
        }
        public void CallEventHandsEmpty() {
            if (EventHandsEmpty != null) {
                EventHandsEmpty();
            }
        }
        public void CallEventAmmoChanged() {
            if (EventAmmoChanged != null) {
                EventAmmoChanged();
            }
        }
        public void CallEventPickedUpAmmo(string ammoName, int quantity) {
            if (EventPickedUpAmmo != null) {
                EventPickedUpAmmo(ammoName, quantity);
            }
        }
        public void CallEventPlayerHealthReduce(int dmg) {
            if (EventPlayerHealthReduce != null) {
                EventPlayerHealthReduce(dmg);
            }
        }
        public void CallEventPlayerHealthIncrease(int heal) {
            if (EventPlayerHealthIncrease != null) {
                EventPlayerHealthIncrease(heal);
            }
        }
    }
}

