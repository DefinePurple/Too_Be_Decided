using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Player_Death : MonoBehaviour {

        private Player_Master playerMaster;

        void OnEnable() {
            SetInitial();
            playerMaster.EventDeath += Respawn;
        }

        void OnDisable() {
            playerMaster.EventDeath += Respawn;
        }
        
        void SetInitial() {
            playerMaster = GetComponent<Player_Master>();
        }

        void Update() {
            if (transform.position.y < -50) {
                Respawn();
            }
        }

        void Respawn() {
            GameObject[] allSpawners = GameObject.FindGameObjectsWithTag("Spawn");
            GameObject spawn = null;
            while (spawn == null) {
                int i = (int) Random.Range(0, allSpawners.Length);
                if (allSpawners[i].GetComponent<SpawnPoint>().playerPeerId == int.MaxValue) {
                    spawn = allSpawners[i];
                }
            }
            this.transform.position = spawn.transform.position;
            this.transform.rotation = spawn.transform.rotation;
        }
    }
}