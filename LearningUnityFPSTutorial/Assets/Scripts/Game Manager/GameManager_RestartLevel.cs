using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace TBD {
    public class GameManager_RestartLevel : MonoBehaviour {

        private GameManager_Master gameManagerMaster;

        void OnEnable() {
            SetInitial();
            gameManagerMaster.RestartLevelEvent += RestartLevel; 
        }

        void OnDisable() {
            gameManagerMaster.RestartLevelEvent -= RestartLevel;
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }  
    }
}