using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Item_LifeTime : MonoBehaviour {

        public float lifeTime = 10;

        // Use this for initialization
        void Start() {
            StartCoroutine(Die());
        }

        private IEnumerator Die() {
            yield return new WaitForSeconds(lifeTime);
            Destroy(this);
        }
    }
}

