﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Item_Pickup : MonoBehaviour {

        private Item_Master itemMaster;
        private Transform myTransform;

        void OnEnable() {
            SetInitial();
            itemMaster.EventPickupAction += CarryOutPickupActions;
        }
        
        void OnDisable() {
            itemMaster.EventPickupAction -= CarryOutPickupActions;
        }

        void SetInitial() {
            itemMaster = GetComponent<Item_Master>();
        }

        void CarryOutPickupActions(Transform tParent) {
            transform.SetParent(tParent);
            itemMaster.CallEventObjectPickup();
            transform.gameObject.SetActive(false);
        }
    }
}

