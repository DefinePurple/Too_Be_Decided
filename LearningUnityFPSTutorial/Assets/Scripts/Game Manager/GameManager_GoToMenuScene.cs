using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBD {
    public class GameManager_GoToMenuScene : MonoBehaviour {

        private GameManager_Master gameManagerMaster;

        void OnEnable() {
            SetInitial();
            gameManagerMaster.GoToMenuSceneEvent += GoToMenuScene;
        }

        void OnDisable() {
            gameManagerMaster.GoToMenuSceneEvent -= GoToMenuScene;
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void GoToMenuScene() {
            SceneManager.LoadScene(0);
        }
    }
}

