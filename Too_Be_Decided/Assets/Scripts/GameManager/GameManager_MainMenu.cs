using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBD {
    public class GameManager_MainMenu : MonoBehaviour {

        private GameManager_Master gameManagerMaster;

        void OnEnable() {
            SetInitial();
            gameManagerMaster.GoToMainMenu += GoToMenuScene;
        }

        void OnDisable() {
            gameManagerMaster.GoToMainMenu -= GoToMenuScene;
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void GoToMenuScene() {
            SceneManager.LoadScene(0);//load menu scene
        }
    }
}

