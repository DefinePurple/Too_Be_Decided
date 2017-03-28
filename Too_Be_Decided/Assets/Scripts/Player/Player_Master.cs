using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.RT;

namespace TBD {
    public class Player_Master : MonoBehaviour {

        private Transform spawnPos;
        private bool isPlayer;

        private Vector3 prevPos, velocity;
        private float updateRate = 0.1f;
        public Vector2 goToPos;
        public float gotoRot;

        private Text myScoreText;
        private int myScore;

        public void Setup(Transform _spawnPos, bool _isPlayer) {
            spawnPos = _spawnPos; // set the spawn position
            isPlayer = _isPlayer; // set the player

            if (_isPlayer) {
                prevPos = transform.position;
                StartCoroutine(SendMovement());
            } else {
                goToPos = this.transform.position;
                gotoRot = this.transform.eulerAngles.z;
            }

            //myScoreText = _myScoreTxt; // set the player's score
            //myScoreText.text = "Score:0"; // we'll also set the text to zero to start with
        }
        
        void Update() {
            #region Player Movement
            if (!isPlayer) { // check that this is the player's tank
                this.transform.position = Vector2.Lerp(transform.position, goToPos, Time.deltaTime / updateRate); // lerp the enemy tank to the new position
                this.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(this.transform.eulerAngles.z, gotoRot, Time.deltaTime / updateRate)); // lerp the enemy tank to the new angle
            }

            velocity = this.transform.position - prevPos; // calculate velocity
            this.GetComponent<Rigidbody>().velocity = Vector2.zero; // cancel any acquired velocity
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //cancel any angular velocity
            #endregion
        }

        private IEnumerator SendMovement() {
            //Checks if the player is actually moving before sending packet
            if ((this.transform.position != prevPos) || (Mathf.Abs(Input.GetAxis("Vertical")) > 0) || (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)) {
                using (RTData data = RTData.Get()) {  // we put a using statement here so that we can dispose of the RTData objects once the packet is sent
                    data.SetVector4(1, new Vector4(this.transform.position.x, this.transform.position.y, velocity.x, velocity.y)); // add the position at key 1
                    data.SetFloat(2, this.transform.eulerAngles.z); // add the rotation at key 2
                    GameSparksManager.Instance().GetRTSession().SendData(2, GameSparks.RT.GameSparksRT.DeliveryIntent.UNRELIABLE_SEQUENCED, data);// send the data
                }
                prevPos = this.transform.position; // record position for any discrepancies
            }

            yield return new WaitForSeconds(updateRate);
            StartCoroutine(SendMovement());
        }
    }
}

