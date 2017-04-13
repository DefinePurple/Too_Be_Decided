using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBD {
    public class GameManager_ExitGame : MonoBehaviour {

        private GameManager_Master gameManagerMaster;

        void OnEnable() {
            SetInitial();
            gameManagerMaster.ExitGame += QuitGame;
        }

        void OnDisable() {
            gameManagerMaster.ExitGame -= QuitGame;
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void QuitGame() {
            Application.Quit();
        }
    }
}

