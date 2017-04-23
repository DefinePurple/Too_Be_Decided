using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TBD {
    public class Gun_AmmoUI : MonoBehaviour {
        public Text currentAmmoText;
        public Text carriedText;

        private Gun_Master gunMaster;

        void OnEnable() {
            SetInitial();
            gunMaster.EventAmmoChanged += UpdateAmmoUI;
        }

        void OnDisable() {
            gunMaster.EventAmmoChanged -= UpdateAmmoUI;
        }

        void SetInitial() {
            gunMaster = GetComponent<Gun_Master>();
        }

        //Updates the ammo text on the UI
        void UpdateAmmoUI(int currentAmmo, int carriedAmmo) {
            if (currentAmmoText != null)
                currentAmmoText.text = currentAmmo.ToString();

            if (carriedText != null)
                carriedText.text = carriedAmmo.ToString();
        }
    }
}

