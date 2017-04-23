using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Item_Master : MonoBehaviour {

        private Player_Master playerMaster;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventObjectPickup;

        public delegate void PickupActionEventHandler(Transform Item);
        public event PickupActionEventHandler EventPickupAction;

        void OnEnable() {
            SetInitial();
        }

        public void CallEventObjectPickup() {
            if (EventObjectPickup != null) {
                EventObjectPickup();
            }
        }

        public void CallEventPickupAction(Transform item) {
            if (EventPickupAction != null) {
                EventPickupAction(item);
            }
        }

        void SetInitial() {
            if (GameManager_References._player != null) {
                playerMaster = GameManager_References._player.GetComponent<Player_Master>();
            }
        }
    }
}

