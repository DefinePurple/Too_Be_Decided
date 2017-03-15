using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Gun_MuzzleFlash : MonoBehaviour {

        public ParticleSystem muzzleFlash;
        private Gun_Master gunMaster;

        void OnEnable() {
            SetInitial();
            gunMaster.EventPlayerInput += PlayMuzzleFlash; 
        }

        void OnDisable() {
            gunMaster.EventPlayerInput -= PlayMuzzleFlash;
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
        }

        void PlayMuzzleFlash() {
            if(muzzleFlash != null) {
                muzzleFlash.Play();
            }
        }
	}
}

