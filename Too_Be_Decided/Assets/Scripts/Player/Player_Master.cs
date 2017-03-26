using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TBD {
    public class Player_Master : MonoBehaviour {

        private Transform spawnPos;
        private bool isPlayer;

        public void Setup(Transform _spawnPos, bool _isPlayer) {
            isPlayer = _isPlayer;
        }
    }
}

