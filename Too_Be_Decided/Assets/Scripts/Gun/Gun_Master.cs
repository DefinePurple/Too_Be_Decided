using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD {
    public class Gun_Master : MonoBehaviour {

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventPlayerInput;//Event for when player shoots
        public event GeneralEventHandler EventGunNotUsable;//Event for when the gun is no longer usable eg: out of ammo
        public event GeneralEventHandler EventReload;//event to reload


        public delegate void GunHitEventHandler(Transform hitTransform);
        public event GunHitEventHandler EventShotDefault;//Event when a shot is fired, always happens
        public event GunHitEventHandler EventShotEnemy;//Event when shot is fired and hits an enemy

        public delegate void GunAmmoEventHandler(int currentAmmo, int carriedAmmo);
        public event GunAmmoEventHandler EventAmmoChanged;//Event for when the ammo changes ie: numbers decrease / increase

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