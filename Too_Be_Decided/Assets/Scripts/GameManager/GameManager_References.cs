using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class GameManager_References : MonoBehaviour {

        public static string _playerTag;
        public static string _enemyTag;
        public static GameObject _player;

        void OnEnable() {
            _playerTag = "Player";
            _enemyTag = "Enemy";

            _player = GameObject.FindGameObjectWithTag(_playerTag);//creates a reference to the player
        }
    }
}

