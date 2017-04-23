using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


namespace TBD {
    public class GameManager_TogglePlayer : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        private FirstPersonController fpsScript;

        void OnDisable() {
            gameManagerMaster.MenuToggleEvent -= TogglePlayer;
        }

        void Start() {
            SetInitial();
            gameManagerMaster.MenuToggleEvent += TogglePlayer;
        }
        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
            fpsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();//gets a reference to the player movement script
        }

        void TogglePlayer() {
            if (fpsScript != null)
                fpsScript.enabled = !fpsScript.enabled;//disable the player movement script if it exists
        }
    }
}

