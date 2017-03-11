using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_HurtCanvas : MonoBehaviour {

        public GameObject hurtCanvas;
        private Player_Master playerMaster;
        private float secondsTillHide = 2;
        
        void OnEnable() {
            SetInitial();
            playerMaster.EventPlayerHealthReduce += TurnOnHurtEffect;
        }

        void OnDisable() {
            playerMaster.EventPlayerHealthReduce -= TurnOnHurtEffect;
        }

        void SetInitial() {
            playerMaster = GetComponent<Player_Master>();
        }

        void TurnOnHurtEffect(int dummy) {
            if(hurtCanvas != null) {
                StopAllCoroutines();
                hurtCanvas.SetActive(true);
                StartCoroutine(ResetCanvas());
            }
        }

        IEnumerator ResetCanvas() {
            yield return new WaitForSeconds(secondsTillHide);
            hurtCanvas.SetActive(false);
        }
	}
}

