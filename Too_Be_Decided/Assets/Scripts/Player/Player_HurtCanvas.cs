using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Player_HurtCanvas : MonoBehaviour {

        public GameObject hurtCanvas;
        private Player_Master playerMaster;
        private float secondsTillHide = 1;
        
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
            //if the canvas exists
            if(hurtCanvas != null) {
                StopAllCoroutines();//stop everything
                hurtCanvas.SetActive(true);//set the canvas to true
                StartCoroutine(ResetCanvas());//reset the canvas
            }
        }

        IEnumerator ResetCanvas() {
            yield return new WaitForSeconds(secondsTillHide);
            hurtCanvas.SetActive(false);
        }
	}
}

