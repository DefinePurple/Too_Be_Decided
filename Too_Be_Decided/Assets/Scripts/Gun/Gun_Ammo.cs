using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_Ammo : MonoBehaviour {

        private Player_Master playerMaster;
        private Gun_Master gunMaster;
        private Player_AmmoBox ammoBox;

        public int clipSize;
        public int currentAmmo;
        public float reloadTime;

        void OnEnable() {
            SetInitial();
            StartingSanityCheck();
            CheckAmmoStatus();

            gunMaster.EventPlayerInput += DeductAmmo;
            gunMaster.EventPlayerInput += CheckAmmoStatus;
            gunMaster.EventReload += TryToReload;
            gunMaster.EventGunNotUsable += TryToReload;

            if(playerMaster != null) {
                playerMaster.EventAmmoChanged += UIAmmoUpdateRequest;
            }

            if(ammoBox != null) {
                StartCoroutine(UpdateAmmoUIWhenEnabling());
            }
        }

        void OnDisable() {
            gunMaster.EventPlayerInput -= DeductAmmo;
            gunMaster.EventPlayerInput -= CheckAmmoStatus;
            gunMaster.EventReload -= TryToReload;
            gunMaster.EventGunNotUsable -= TryToReload;

            if (playerMaster != null) {
                playerMaster.EventAmmoChanged -= UIAmmoUpdateRequest;
            }
        }

        void Start() {
            SetInitial();
            StartCoroutine(UpdateAmmoUIWhenEnabling());
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
            playerMaster = GetComponentInParent<Player_Master>();
            ammoBox = GetComponentInParent<Player_AmmoBox>();
            
        }

        void DeductAmmo() {
            currentAmmo--;
            UIAmmoUpdateRequest();
        }

        //tries to reload
        void TryToReload() {
            //Conditions:
            //Ammo on person must not be zero
            //Clip cant be full
            //gun cant already be reloading
            if(ammoBox.ammoCurrentCarried != 0 && currentAmmo != clipSize && !gunMaster.isReloading) {
                gunMaster.isReloading = true;
                gunMaster.isGunLoaded = false;
                StartCoroutine(ReloadWithoutAnimation());
            }
        }

        //checks if the gun is loaded
        void CheckAmmoStatus() {
            if (currentAmmo <= 0) {
                currentAmmo = 0;
                gunMaster.isGunLoaded = false;
            } else if (currentAmmo > 0)
                gunMaster.isGunLoaded = true;
        }

        //Checks if the current amount in the clip is not more than it can handle
        void StartingSanityCheck() {
            if(currentAmmo > clipSize) {
                currentAmmo = clipSize;
            }
        }

        void UIAmmoUpdateRequest() {
            gunMaster.CallEventAmmoChanged(currentAmmo, ammoBox.ammoCurrentCarried);
        }

        //Reset gun, IE: no longer loading and is loaded.
        void ResetGunReloading() {
            gunMaster.isReloading = false;
            CheckAmmoStatus();
            UIAmmoUpdateRequest();
        }

        void OnReloadComplete() {
            //Gets difference in ammo
            int ammo = clipSize - currentAmmo;

            //if there is enough ammo
            if(ammoBox.ammoCurrentCarried >= ammo) {
                currentAmmo += ammo;//adds ammo to current clip
                ammoBox.ammoCurrentCarried -= ammo;//removes ammo from max carried
            }
            //if there is not enough ammo, just add whats left to the clip
            else if(ammoBox.ammoCurrentCarried < ammo && ammoBox.ammoCurrentCarried != 0) {
                currentAmmo += ammoBox.ammoCurrentCarried;
                ammoBox.ammoCurrentCarried = 0;
            }

            ResetGunReloading();//resets gun
        }

        //Calls on complete when reloading is finished
        IEnumerator ReloadWithoutAnimation() {
            yield return new WaitForSeconds(reloadTime);
            OnReloadComplete();
        }

        // Ensures UI is updated when starting
        IEnumerator UpdateAmmoUIWhenEnabling() {
            yield return new WaitForSeconds(0.5f);
            UIAmmoUpdateRequest();
        }
    }
}

