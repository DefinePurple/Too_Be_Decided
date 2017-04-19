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
        }

        void OnDisable() {
            playerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
            playerMaster.EventPlayerHealthReduce -= DeductHealth;
        }

        void Start() {
            //StartCoroutine(TestHealthDeduction());
        }

        void SetInitial() {
            gameManagerMaster = GameObject.Find("GameController").GetComponent<GameManager_Master>();
            playerMaster = GetComponent<Player_Master>();
        }

        IEnumerator TestHealthDeduction() {
            yield return new WaitForSeconds(1f);
            playerMaster.CallEventPlayerHealthReduce(50);
        }

        void DeductHealth(int healthChange) {
            playerHealth -= healthChange;

            if (playerHealth <= 0) {
                playerHealth = 0;
            }

            SetUI();
            Debug.Log("Setting health to " + playerHealth);
        }

        void IncreaseHealth(int healthChange) {
            playerHealth += healthChange;

            if (playerHealth > 100) {
                playerHealth = 100;
            }

            SetUI();
        }

        void SetUI() {
            if (healthText != null) {
                healthText.text = playerHealth.ToString();
                Debug.Log("Setting health to " + playerHealth);
            }
        }
    }
}
