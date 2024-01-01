//vscode "editor.codeLens" needs to be off for better view

using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class FpsShooting : MonoBehaviour
{
    public GameObject impactEffect;
    RaycastHit hit;
    public GameObject RayPoint;
    public bool CanFire = true;
    float gunTimer;
    public float gunCooldown = 0.1f;
    public ParticleSystem MuzzleFlash;

    [Header("Oyun Sesleri")]
    AudioSource SesKaynak;
    public AudioClip FireSound;
    public float range = 500;


    public int maxAmmo = 999;
    public int ammoInPocket = 120;
    public int ammoInGun = 30;
    public int magazineCapacity = 30;
    public float reloadCooldown = 2;

    // Start is called before the first frame update
    void Start()
    {
        SesKaynak = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    //void Update() {}

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanFire == true && Time.time > gunTimer && ammoInGun > 0)
        {
            Fire();
            gunTimer = Time.time + gunCooldown;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }
    }

    void Fire()
    {
        ammoInGun -= 1;
        MuzzleFlash.Play();
        SesKaynak.Play();
        SesKaynak.clip = FireSound;
        if (Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, range))
        {

            //Debug.Log(hit.transform.name);
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Debug.LogFormat("Ammo Amount: " + ammoInGun.ToString());
        }
    }

    public void ReloadGun()
    {
        if (ammoInPocket < 1) { print("Not enough ammo."); return; }
        int neededAmmoAmount = Mathf.Min(ammoInPocket, magazineCapacity - ammoInGun);

        if (neededAmmoAmount < 1) { print("No need for reload."); return; }
        ammoInGun += neededAmmoAmount;
        ammoInPocket -= neededAmmoAmount;
    }
}
