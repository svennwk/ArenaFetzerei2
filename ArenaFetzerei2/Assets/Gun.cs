using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int damage = 20;
    public double shootInterval = 0.5f;
    public int maxAmmo = 6;
    public float reloadTime = 2f;
    public Animator animator;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private int currAmmo;
    private double nextTimeToFire = 0;
    private bool isReloading = false;

    public Camera fpsCam;


    void Start()
    {
        currAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update() {

        //Check if Reloading
        if (isReloading) return;

        //Reload
        if (currAmmo <= 0) {
            StartCoroutine(Reload());
            return;
        }

        //Fire input
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + shootInterval;
            Debug.Log("Shoot");
            Shoot();
        }

    }

    IEnumerator Reload() {

        isReloading = true;
        Debug.Log("Reloading..");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);


        currAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot() {

        currAmmo--;
        muzzleFlash.Play();
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
            //Debug.Log(hit.transform.name);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) {
                target.TakeDamage(damage);
                Debug.Log(target.GetHealth());
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);
        }
    }


}
