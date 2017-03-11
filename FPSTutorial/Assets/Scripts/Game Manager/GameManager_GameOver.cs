using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class GameManager_GameOver : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        public GameObject gameOverPanel;

        void OnEnable() {
            SetInitial();
            gameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
        }

        void OnDisable() {
            gameManagerMaster.GameOverEvent -= TurnOnGameOverPanel;
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TurnOnGameOverPanel() {
            if(gameOverPanel != null) {
                gameOverPanel.SetActive(true);
            }
        }
    }
}

