using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class GameManager_ToggleMenu : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        public GameObject menu;

        // Update is called once per frame
        void Update() {
            CheckForMenuToggle();
        }

        void OnEnable() {
            SetInitial();
        }

        void OnDisable() {
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void CheckForMenuToggle() {
            if (Input.GetButtonDown("Menu")) {
                ToggleMenu();
            }
        }

        void ToggleMenu() {
            if (menu != null) {
                menu.SetActive(!menu.activeSelf);
                gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
                gameManagerMaster.CallEventMenuToggle();
            }
        }
    }
}