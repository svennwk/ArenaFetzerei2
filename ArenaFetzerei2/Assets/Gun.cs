using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int damage = 20;
    public float shootInterval = 0.5f;
    public int maxAmmo = 6;
    public float reloadTime = 2f;
    public Animator animator;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private int currAmmo;
    private double nextTimeToFire = 0;
    private bool isReloading = false;
    private bool canShoot = true;

    public Camera fpsCam;


    void Start()
    {
        currAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
        animator.SetBool("Fire", false);
    }

    void Update() {

        //Check if Reloading
        if (isReloading) return;

        //Checks magazin is empty
        if (currAmmo <= 0) {
            canShoot = false;
        }

        //Fire input
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && canShoot) {
            nextTimeToFire = Time.time + shootInterval;
            StartCoroutine(Shoot());
        }

        //Reloading
        if(Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Reload");
            StartCoroutine(Reload());
        }

    }

    IEnumerator Reload() {

        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);


        currAmmo = maxAmmo;
        isReloading = false;
        canShoot = true;
    }



    IEnumerator Shoot() {

        currAmmo--;
        muzzleFlash.Play();
        RaycastHit hit;
        //Debug.Log("Shoot");
        animator.SetBool("Fire", true);
        yield return new WaitForSeconds(shootInterval);
        animator.SetBool("Fire", false);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
            //Debug.Log(hit.transform.name);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) {
                target.TakeDamage(damage);
                //Debug.Log(target.GetHealth());
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);
        }
    }


}
