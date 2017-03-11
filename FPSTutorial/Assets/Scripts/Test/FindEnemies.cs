using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class FindEnemies : MonoBehaviour {

        GameObject[] enemies;

        // Use this for initialization
        void Start() {
            SearchForEnemies();
        }

        // Update is called once per frame
        void Update() {

        }

        void SearchForEnemies() {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if(enemies.Length > 0) {
                foreach (GameObject go in enemies) {
                    Debug.Log(go.name);
                    //go.SetActive(false);
                }
            }
        }
    }
}

