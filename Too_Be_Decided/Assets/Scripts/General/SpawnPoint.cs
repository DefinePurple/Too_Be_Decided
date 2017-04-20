using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class SpawnPoint : MonoBehaviour {

        public int playerPeerId = int.MaxValue;

        void Start() {
            StartCoroutine(EnableSpawn());
        }

        private IEnumerator EnableSpawn() {
            yield return new WaitForSeconds(3);
            if (playerPeerId != int.MaxValue) {
                playerPeerId = int.MaxValue;
            }
        }
	}
}