using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_DisableBody : MonoBehaviour {

        private Player_Master playerMaster;
        public GameObject body;

		void OnEnable() {
            SetInitial();
            if (playerMaster.IsPlayer()) {
                body.SetActive(false);
            }
        }

        void SetInitial() {
            playerMaster = GetComponent<Player_Master>();
        }
	}
}

