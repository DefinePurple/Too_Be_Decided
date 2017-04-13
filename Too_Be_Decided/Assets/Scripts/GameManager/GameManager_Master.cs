using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class GameManager_Master : MonoBehaviour {

        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler MenuToggleEvent;
        public event GameManagerEventHandler GoToMainMenu;
        public event GameManagerEventHandler ExitGame;

        public bool isMenuOn;

        public void CallEventMenuToggle() {
            if (MenuToggleEvent != null) {
                MenuToggleEvent();
            }
        }

        public void CallEventGoToMainMenu() {
            if (GoToMainMenu != null) {
                GoToMainMenu();
            }
        }

        public void CallEventExitGame() {
            if (ExitGame != null) {
                ExitGame();
            }
        }
    }
}

