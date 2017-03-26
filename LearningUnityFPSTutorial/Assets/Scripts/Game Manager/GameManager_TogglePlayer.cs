using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace TBD {
    public class GameManager_TogglePlayer : MonoBehaviour {

        public FirstPersonController playerController;
        private GameManager_Master gameManagerMaster;

        void OnEnable() {
            SetInitial();
            gameManagerMaster.MenuToggleEvent += TogglePlayerController;
        }

        void OnDisable() {
            gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TogglePlayerController() {
            if(playerController != null) {
                playerController.enabled = !playerController.enabled;
            }
        }

    }
}

