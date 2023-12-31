using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsShooting : MonoBehaviour
{
    RaycastHit hit;
    public GameObject RayPoint;
    public bool CamFire;
    float gunTimer;
    public float gunCooldown;
    public ParticleSystem MuzzleFlash;

    [Header("Oyun Sesleri")]
    AudioSource SesKaynak;
    public AudioClip FireSound;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        SesKaynak = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CamFire == true && Time.time > gunTimer)
        {
            Fire();
            gunTimer = Time.time + gunCooldown;
        }
    }    
    // Start is called before the first frame update
    void Fire()
    {

        if (Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, range))
        {
            MuzzleFlash.Play();
            SesKaynak.Play();
            SesKaynak.clip = FireSound;
            Debug.Log(hit.transform.name);
        }
    }
}
