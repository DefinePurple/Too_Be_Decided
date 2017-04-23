using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_MuzzleFlash : MonoBehaviour {

        public ParticleSystem particles;
        private Gun_Master gunMaster;

        void OnEnable() {
            SetInitial();
            gunMaster.EventPlayerInput += PlayMuzzleFlash;
        }

        void OnDisbale() {
            gunMaster.EventPlayerInput -= PlayMuzzleFlash;
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
        }

        //called when the player shoots. Plays the particle effect
        void PlayMuzzleFlash() {
            if (particles != null)
                particles.Play();
        }
    }
}

