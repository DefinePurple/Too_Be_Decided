using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class GameManager_Master : MonoBehaviour {

        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler MenuToggleEvent;

        public bool isMenuOn;

        public void CallEventMenuToggle() {
            if (MenuToggleEvent != null) {
                MenuToggleEvent();
            }
        }
    }
}

