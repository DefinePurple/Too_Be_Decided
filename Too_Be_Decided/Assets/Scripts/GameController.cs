using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class GameController : MonoBehaviour {

        private static GameController instance; // this is the singleton for the game-controller class
        public static GameController Instance() {
            return instance;
        }
        void Awake() {
            instance = this;
        }
    }
}

