using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace TBD {
    public class Player_Master : MonoBehaviour {

        private Transform spawnPos;
        private bool isPlayer;

        public void SetupTank(Transform _spawnPos, bool _isPlayer, Text _myScoreTxt) {
            //public void SetupTank(Transform _spawnPos, bool _isPlayer) {
            isPlayer = _isPlayer;
           
            if (!isPlayer)
                GetComponent<FirstPersonController>().enabled = !enabled;
        }
    }
}

