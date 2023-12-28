using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int maxAmmo = 150;
    public int totalAmmo = 90;
    public int magazineCapacity = 30;
    public int ammoInMagazine = 30;
    public int pickUpAmount = 30;
    public int shootingDelayInMS = 100;
    bool canShoot = true;
    int fixedTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickUpAmmo() {
        totalAmmo = Mathf.Min((totalAmmo+pickUpAmount), maxAmmo);
    }

    public void fire() {
        if (canShoot == true && ammoInMagazine > 0) {
            ammoInMagazine -= 1;
            fixedTimer = 0;
            canShoot = false;
        }
    }

    void FixedUpdate() {
        fixedTimer += 1;
        if (fixedTimer>3) {
            canShoot = true;
        }
    }

    public void reloadGun() {
        if (totalAmmo<1) {print("Not enough ammo."); return;}
        int neededAmmoAmount = Mathf.Min(magazineCapacity - ammoInMagazine, totalAmmo);
        if (neededAmmoAmount < 1) {print("No need for reload."); return;}
        ammoInMagazine += neededAmmoAmount;
        totalAmmo -= neededAmmoAmount;
    }

}
