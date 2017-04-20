using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class DeathCube : MonoBehaviour {

        // Use this for initialization
        void Start() {
            StartCoroutine(Die());
        }

        private IEnumerator Die() {
            yield return new WaitForSeconds(10);
            Destroy(this);
        }
    }
}

