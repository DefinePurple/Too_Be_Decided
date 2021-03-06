﻿using System.Collections;
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
            GameObject[] allSpawners = GameObject.FindGameObjectsWithTag("Spawn");//get all spawners
            GameObject spawn = null;
            while (spawn == null) {
                int i = (int) Random.Range(0, allSpawners.Length);//pick a random spawner
                if (allSpawners[i].GetComponent<SpawnPoint>().playerPeerId == int.MaxValue) {//if the spawner can spawn a player, choose it
                    spawn = allSpawners[i];
                }
            }
            spawn.GetComponent<SpawnPoint>().StartCountdown();//reset the spawner

            //set the players position and rotation to that of the spawner
            this.transform.position = spawn.transform.position;
            this.transform.rotation = spawn.transform.rotation;
        }
    }
}