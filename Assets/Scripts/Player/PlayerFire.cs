using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;

    public float fireRate = 5;

    private bool fire = true;
    private float counter;

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("Fire1") > 0 && fire) {
            GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab);
            bullet.transform.position = bulletSpawnPoint.transform.position;
            bullet.transform.rotation = bulletSpawnPoint.transform.rotation;
            counter = fireRate;
            fire = false;
        }

        if (counter <= 0 || Input.GetAxis("Fire1") == 0) {
            fire = true;
        }

        counter -= Time.deltaTime;
    }
}
