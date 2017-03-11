using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class TriggerExample : MonoBehaviour {
        private GameManager_EventSystem eventScript;

        void OnEnable() {
            SetInitial();
        }

        void OnTriggerEnter(Collider col) {
            eventScript.CallMyGeneralEvent();
            Destroy(gameObject);
        }

        void SetInitial() {
            eventScript = GameObject.Find("GameManager").GetComponent<GameManager_EventSystem>();
        }
    }
}

