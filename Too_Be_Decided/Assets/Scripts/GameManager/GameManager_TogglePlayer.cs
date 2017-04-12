using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


namespace TBD {
    public class GameManager_TogglePlayer : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        private FirstPersonController fpsScript;

        void OnEnable() {
            SetInitial();

            gameManagerMaster.MenuToggleEvent += TogglePlayer;
        }

        void OnDisable() {
            gameManagerMaster.MenuToggleEvent -= TogglePlayer;
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
            fpsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        }

        void TogglePlayer() {
            if (fpsScript != null)
                fpsScript.enabled = !fpsScript.enabled;
        }
    }
}

