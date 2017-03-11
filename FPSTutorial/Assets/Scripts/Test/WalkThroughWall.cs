using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class WalkThroughWall : MonoBehaviour {
        private Color myColor = new Color(0.5f, 0.5f, 0.3f);
        private GameManager_EventSystem eventScript;

        void Start() {
            SetInitial();
        }


        void OnEnable() {
            eventScript.myGeneralEvent += SetLayerToNotSolid;
        }

        void OnDisable() {
            eventScript.myGeneralEvent -= SetLayerToNotSolid;
        }

        public void SetLayerToNotSolid() {
            gameObject.layer = LayerMask.NameToLayer("Not Solid");
            GetComponent<Renderer>().material.SetColor("_Color", myColor);
        }

        void SetInitial() {
            eventScript = GameObject.Find("GameManager").GetComponent<GameManager_EventSystem>();
        }
    }
}

