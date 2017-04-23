﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Item_Ammo : MonoBehaviour {

        private Item_Master itemMaster;
        private GameObject playerGO;
        public int quantity;

        void OnEnable() {
            SetInitial();
            itemMaster.EventObjectPickup += TakeAmmo;
        }

        void Start() {
            SetInitial();
        }

        void OnDisable() {
            itemMaster.EventObjectPickup -= TakeAmmo;
        }

        void SetInitial() {
            itemMaster = GetComponent<Item_Master>();
            playerGO = GameObject.FindGameObjectWithTag("Player");
        }

        void TakeAmmo() {
            playerGO.GetComponent<Player_Master>().CallEventPickedUpAmmo(quantity);
            Destroy(gameObject);
        }
    }
}