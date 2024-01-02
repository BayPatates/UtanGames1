//vscode "editor.codeLens" needs to be off for better view

using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FpsShooting : MonoBehaviour
{
    float gunTimer;
    float reloadTimer;
    public GameObject impactEffect;
    public GameObject BloodyimpactEffect;

    RaycastHit hit;
    RaycastHit pickUpHit;
    public GameObject RayPoint;
    public GameObject mainRayPoint;
    public bool CanFire = true;
    public float gunCooldown = 0.1f;
    public ParticleSystem MuzzleFlash;
    public float pickUpRange = 1.5f;

    public CharacterController Karakter;
    public Animator GunAnimset;

    AudioSource SesKaynak;
    [Header("Ses Klipleri")]
    public AudioClip FireSound;
    public AudioClip ReloadSound;

    [Header("Silah Özellikleri")]
    public float range = 500;
    public int maxAmmo = 150;
    public int ammoInPocket = 120;
    public int ammoInGun = 30;
    public int magazineCapacity = 30;
    public float reloadCooldown = 2;
    public float damage;

    [Header("UI Elementleri")]
    public TextMeshProUGUI ammoCounterText;

    // Start is called before the first frame update
    void Start()
    {
        SesKaynak = GetComponent<AudioSource>();
        UpdateAmmoCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanFire == true && Time.time > gunTimer && ammoInGun > 0)
        {
            Fire();
            gunTimer = Time.time + gunCooldown;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Time.time > reloadTimer)
            {
                StartCoroutine(ReloadGun());
            }
        }

        if (Physics.Raycast(mainRayPoint.transform.position, mainRayPoint.transform.forward, out pickUpHit, pickUpRange))
        {
            if (Input.GetKeyDown(KeyCode.E) && pickUpHit.collider.gameObject.tag == "ammo")
            {
                ammoInPocket += magazineCapacity;
                ammoInPocket = Mathf.Min(ammoInPocket, maxAmmo);
                UpdateAmmoCounter();
            }
            //Debug.Log(hit.transform.name);
            //Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    void Fire()
    {
        SesKaynak.PlayOneShot(FireSound);
        ammoInGun -= 1;
        UpdateAmmoCounter();
        MuzzleFlash.Play();
        SesKaynak.Play();
        if (Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, range))
        {


        if (hit.collider.tag == "Untagged")
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else if (hit.collider.tag == "Enemy")
        {
            Instantiate(BloodyimpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            hit.collider.gameObject.transform.root.GetComponent<ZombieScript>().health = hit.collider.gameObject.transform.root.GetComponent<ZombieScript>().health - damage;
        }


        }
    }

    IEnumerator ReloadGun()
    {
        //Reload gerekip gerekmediğini test et ve ne kadar gerektiğini hesapla.
        //Ayrıca kullanıcıyı bilgilendir.
        if (ammoInPocket < 1)
        {
            //print("Not enough ammo.");
            ammoCounterText.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            ammoCounterText.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            ammoCounterText.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            ammoCounterText.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            yield break;
        }
        int neededAmmoAmount = Mathf.Min(ammoInPocket, magazineCapacity - ammoInGun);
        if (neededAmmoAmount < 1)
        {
            //print("No need for reload.");
            ammoCounterText.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            ammoCounterText.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            ammoCounterText.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            ammoCounterText.color = Color.white;
            yield return new WaitForSeconds(0.2f);
            yield break;
        }

        //Reload etmeye başla.
        //Debug.Log("Reloading...");
        GunAnimset.Play("Reload");
        GunAnimset.SetBool("isReloading", true);
        reloadTimer = Time.time + reloadCooldown;
        CanFire = false;
        SesKaynak.PlayOneShot(ReloadSound);
        yield return new WaitForSeconds(reloadCooldown);

        ammoInGun += neededAmmoAmount;
        ammoInPocket -= neededAmmoAmount;

        UpdateAmmoCounter();
        CanFire = true;
        GunAnimset.SetBool("isReloading", false);
        //Debug.Log("Reload Complete!");
    }

    //Kanvas'ta bulunan mermi sayısını güncelle
    void UpdateAmmoCounter()
    {
        ammoCounterText.text = ammoInGun.ToString() + "/" + ammoInPocket.ToString();
    }
}
