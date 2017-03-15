using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class GameManager_ToggleMenu : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        public GameObject menu;

        // Use this for initialization
        void Start() {
            ToggleMenu();
        }

        // Update is called once per frame
        void Update() {
            CheckForMenuToggle();
        }

        void OnEnable() {
            SetInitial();
            gameManagerMaster.GameOverEvent += ToggleMenu;
        }

        void OnDisable() {

        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
            gameManagerMaster.GameOverEvent += ToggleMenu;
        }

        void CheckForMenuToggle() {
            if (Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn) {
                ToggleMenu();
            }
        }

        void ToggleMenu() {
            if(menu != null) {
                menu.SetActive(!menu.activeSelf);
                gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
                gameManagerMaster.CallEventMenuToggle();
            }
        }
    }
}