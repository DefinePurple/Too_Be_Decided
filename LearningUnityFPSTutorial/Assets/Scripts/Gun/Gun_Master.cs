using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBD{
	public class Gun_Master : MonoBehaviour {

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventPlayerInput;
        public event GeneralEventHandler EventGunNotUsable;
        public event GeneralEventHandler EventRequestReload;
        public event GeneralEventHandler EventGunReset;

        public delegate void GunHitEventHandler(Vector3 hitPosition, Transform hitTransform);
        public event GunHitEventHandler EventShotDefault;
        public event GunHitEventHandler EventShotEnemy;

        public delegate void GunCrosshairEventHandler(float speed);
        public event GunCrosshairEventHandler EventSpeedCaptured;

        public bool isGunLoaded;
        public bool isReloading;

        public void CallEventPlayerInput() {
            if(EventPlayerInput != null) {
                EventPlayerInput();
            }
        }
        public void CallEventGunNotUsable() {
            if(EventGunNotUsable != null) {
                EventGunNotUsable();
            }
        }
        public void CallEventRequestReload() {
            if(EventRequestReload != null) {
                EventRequestReload();
            }
        }
        public void CallEventGunReset() {
            if(EventGunReset != null) {
                EventGunReset();
            }
        }
        public void CallEventShotDefault(Vector3 hitPos, Transform hitTrans) {
            if(EventShotDefault != null) {
                EventShotDefault(hitPos, hitTrans);
            }
        }
        public void CallEventShotEnemy(Vector3 hitPos, Transform hitTrans) {
            if (EventShotEnemy != null) {
                EventShotEnemy(hitPos, hitTrans);
            }
        }
        public void CallEventSpeedCaptured(float speed) {
            if(EventSpeedCaptured != null) {
                EventSpeedCaptured(speed);
            }
        }
    }
}

