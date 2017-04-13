using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.RT;

namespace TBD {
    public class Player_Master : MonoBehaviour {

        #region Events
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventHandsEmpty;
        public event GeneralEventHandler EventAmmoChanged;

        public delegate void AmmoPickupEventHandler(int quantity);
        public event AmmoPickupEventHandler EventPickedUpAmmo;

        public delegate void PlayerHealthEventHandler(int healthChange);
        public event PlayerHealthEventHandler EventPlayerHealthReduce;
        public event PlayerHealthEventHandler EventPlayerHealthIncrease;

        public void CallEventHandsEmpty() {
            if (EventHandsEmpty != null) {
                EventHandsEmpty();
            }
        }
        public void CallEventAmmoChanged() {
            if (EventAmmoChanged != null) {
                EventAmmoChanged();
            }
        }
        public void CallEventPickedUpAmmo(int quantity) {
            if (EventPickedUpAmmo != null) {
                EventPickedUpAmmo(quantity);
            }
        }
        public void CallEventPlayerHealthReduce(int dmg) {
            if (EventPlayerHealthReduce != null) {
                EventPlayerHealthReduce(dmg);
            }
        }
        public void CallEventPlayerHealthIncrease(int heal) {
            if (EventPlayerHealthIncrease != null) {
                EventPlayerHealthIncrease(heal);
            }
        }
        #endregion

        #region GameSparks 
        private Transform spawnPos;
        private bool isPlayer;
        public bool IsPlayer() {
            return isPlayer;
        }

        private Vector3 prevPos, velocity;
        private float updateRate = 0.05f;
        public Vector3 goToPos;
        public float gotoRot;

        private Text myScoreText;
        private int myScore;

        public void Setup(Transform _spawnPos, bool _isPlayer) {
            spawnPos = _spawnPos; // set the spawn position
            isPlayer = _isPlayer; // set the player

            if (isPlayer) {
                prevPos = transform.position;
                StartCoroutine(SendMovement());
            } else {
                goToPos = this.transform.position;
                gotoRot = this.transform.eulerAngles.y;
            }

            //myScoreText = _myScoreTxt; // set the player's score
            //myScoreText.text = "Score:0"; // we'll also set the text to zero to start with
        }

        void Update() {
            #region Player Movement
            if (!isPlayer) { // check that this is the player's tank
                this.transform.position = Vector3.Lerp(transform.position, goToPos, Time.deltaTime / updateRate); // lerp the enemy tank to the new position
                //this.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(this.transform.eulerAngles.z, gotoRot, Time.deltaTime / updateRate)); // lerp the enemy tank to the new angle
                //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, gotoRot.rotation, Time.deltaTime / updateRate);
                this.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(this.transform.eulerAngles.z, gotoRot, Time.deltaTime / updateRate));
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
                    data.SetVector3(1, new Vector4(this.transform.position.x, this.transform.position.y, this.transform.position.z)); // add the position at key 1
                    data.SetVector3(2, new Vector3(velocity.x, velocity.y, 0));
                    data.SetFloat(3, this.transform.rotation.y); // add the rotation at key 2
                    GameSparksManager.Instance().GetRTSession().SendData(2, GameSparks.RT.GameSparksRT.DeliveryIntent.UNRELIABLE_SEQUENCED, data);// send the data
                }
                prevPos = this.transform.position; // record position for any discrepancies
            }

            yield return new WaitForSeconds(updateRate);
            StartCoroutine(SendMovement());
        }
        #endregion
    }
}

