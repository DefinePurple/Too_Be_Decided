using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TBD {
    public class Player_Health : MonoBehaviour {
        private GameManager_Master gameManagerMaster;
        private Player_Master playerMaster;
        public int playerHealth;
        public Text healthText;

        void OnEnable() {
            SetInitial();
            SetUI();
            playerMaster.EventPlayerHealthIncrease += IncreaseHealth;
            playerMaster.EventPlayerHealthReduce += DeductHealth;
            playerMaster.EventDeath += ResetPlayerHealth;
        }

        void OnDisable() {
            playerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
            playerMaster.EventPlayerHealthReduce -= DeductHealth;
            playerMaster.EventDeath += ResetPlayerHealth;
        }

        void SetInitial() {
            gameManagerMaster = GameObject.Find("GameController").GetComponent<GameManager_Master>();
            playerMaster = GetComponent<Player_Master>();
        }

        //reduces the players health
        void DeductHealth(int healthChange) {
            playerHealth -= healthChange;

            if (playerHealth <= 0) {
                playerHealth = 0;
                playerMaster.SendDeath();
                playerMaster.CallEventDeath();
            }

            SetUI();
        }

        //increases the players health
        void IncreaseHealth(int healthChange) {
            playerHealth += healthChange;

            if (playerHealth > 100) {
                playerHealth = 100;
            }

            SetUI();
        }

        void ResetPlayerHealth() {
            playerHealth = 100;
            SetUI();
        }

        void SetUI() {
            if (healthText != null) {
                healthText.text = playerHealth.ToString();
            }
        }
    }
}
