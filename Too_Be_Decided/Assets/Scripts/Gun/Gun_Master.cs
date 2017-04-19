using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_Master : MonoBehaviour {

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventPlayerInput;
        public event GeneralEventHandler EventGunNotUsable;
        public event GeneralEventHandler EventReload;


        public delegate void GunHitEventHandler(Transform hitTransform);
        public event GunHitEventHandler EventShotDefault;
        public event GunHitEventHandler EventShotEnemy;

        public delegate void GunAmmoEventHandler(int currentAmmo, int carriedAmmo);
        public event GunAmmoEventHandler EventAmmoChanged;

        public bool isGunLoaded;
        public bool isReloading;

        public void CallEventPlayerInput() {
            if (EventPlayerInput != null)
                EventPlayerInput();
        }

        public void CallEventGunNotUsable() {
            if (EventGunNotUsable != null)
                EventGunNotUsable();
        }

        public void CallEventReload() {
            if (EventReload != null)
                EventReload();
        }

        public void CallEventShotDefault(Transform hTransform) {
            if (EventShotDefault != null)
                EventShotDefault(hTransform);
        }

        public void CallEventShotEnemy(Transform hTransform) {
            if (EventShotEnemy != null)
                EventShotEnemy(hTransform);
        }

        public void CallEventAmmoChanged(int currentAmmo, int carriedAmmo) {
            if (EventAmmoChanged != null)
                EventAmmoChanged(currentAmmo, carriedAmmo);
        }
    }
}