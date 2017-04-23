using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class SpawnPoint : MonoBehaviour {
        
        //Script for player spawn points
        //Spawn point will hold the player's id for 3 seconds  and then reset itself
        //this is to stop players spawning on top of each other

        public int playerPeerId = int.MaxValue;

        public void StartCountdown() {
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