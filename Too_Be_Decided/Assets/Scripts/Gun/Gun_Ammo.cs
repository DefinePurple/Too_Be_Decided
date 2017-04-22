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
            UIAmmoUpdateRequest();
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
            if (GameManager_References._player != null) {
                playerMaster = GameManager_References._player.GetComponent<Player_Master>();
                ammoBox = GameManager_References._player.GetComponent<Player_AmmoBox>();
            }
        }

        void DeductAmmo() {
            currentAmmo--;
            UIAmmoUpdateRequest();
        }

        void TryToReload() {
            if(ammoBox.ammoCurrentCarried != 0 && currentAmmo != clipSize && !gunMaster.isReloading) {
                gunMaster.isReloading = true;
                gunMaster.isGunLoaded = false;
                StartCoroutine(ReloadWithoutAnimation());
            }
        }

        void CheckAmmoStatus() {
            if (currentAmmo <= 0) {
                currentAmmo = 0;
                gunMaster.isGunLoaded = false;
            } else if (currentAmmo > 0)
                gunMaster.isGunLoaded = true;
        }

        void StartingSanityCheck() {
            if(currentAmmo > clipSize) {
                currentAmmo = clipSize;
            }
        }

        void UIAmmoUpdateRequest() {
            gunMaster.CallEventAmmoChanged(currentAmmo, ammoBox.ammoCurrentCarried);
        }

        void ResetGunReloading() {
            gunMaster.isReloading = false;
            CheckAmmoStatus();
            UIAmmoUpdateRequest();
        }

        void OnReloadComplete() {
            int ammo = clipSize - currentAmmo;

            if(ammoBox.ammoCurrentCarried >= ammo) {
                currentAmmo += ammo;
                ammoBox.ammoCurrentCarried -= ammo;
            } else if(ammoBox.ammoCurrentCarried < ammo && ammoBox.ammoCurrentCarried != 0) {
                currentAmmo += ammoBox.ammoCurrentCarried;
                ammoBox.ammoCurrentCarried = 0;
            }

            ResetGunReloading();
        }

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

