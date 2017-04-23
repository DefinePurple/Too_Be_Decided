using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class GameManager_ToggleCursor : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        private bool isCursorLocked = true;

        void OnEnable() {
            SetInitial();
            gameManagerMaster.MenuToggleEvent += ToggleCursorState;
        }

        void OnDisable() {
            gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
        }

        void Update() {
            CheckIfCursorShouldBeLocked();
        }

        void SetInitial() {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void ToggleCursorState() {
            isCursorLocked = !isCursorLocked;
        }

        void CheckIfCursorShouldBeLocked() {
            if (isCursorLocked) {
                Cursor.lockState = CursorLockMode.Locked;//locks the cursor to the center of the screen
                Cursor.visible = false;//hides the cursor
            } else {
                Cursor.lockState = CursorLockMode.None;//removes lock on cursor
                Cursor.visible = true;//enables view of the cursor
            }
        }
    }
}