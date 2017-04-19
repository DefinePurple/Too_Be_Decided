using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_ApplyDamage : MonoBehaviour {

        private Gun_Master gunMaster;
        public int damage = 10;

        void OnEnable() {
            SetInitial();
            gunMaster.EventShotEnemy += ApplyDamage;
        }

        void OnDisable() {
            gunMaster.EventShotEnemy -= ApplyDamage;
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
        }

        void ApplyDamage(Transform hitTransform) {
            Debug.Log("Applying Damage outside");
            if (hitTransform.GetComponent<Player_Master>() != null) {
                Debug.Log("Applying Damage");
                Player_Master playerMaster = hitTransform.GetComponent<Player_Master>();
                playerMaster.SendHit(damage, playerMaster.GetPeerID());
            }
        }
    }
}

