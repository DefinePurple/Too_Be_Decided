using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Spawner : MonoBehaviour {

        public GameObject objectToSpawn;
        public int numOfEnemies;
        private float spawnRadius = 5;
        private Vector3 spawnPosition;
        private GameManager_EventSystem eventScript;

        void OnEnable() {
            SetInitial();
            eventScript.myGeneralEvent += SpawnObject;
        }

        void OnDisable() {
            eventScript.myGeneralEvent -= SpawnObject;
        }

        void SpawnObject() {
            for (int i = 0; i < numOfEnemies; i++) {
                spawnPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }

        void SetInitial() {
            eventScript = GameObject.Find("GameManager").GetComponent<GameManager_EventSystem>();
        }
    }
}

