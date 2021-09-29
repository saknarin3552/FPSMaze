using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunScript : MonoBehaviour
{
    public int gunDamage;
    public Camera cam;
    //public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    float nestTimeToShot;
    public float firerate;

    private int currentAmmo;
    public int maxAmmo = 10;
    public float reloadTime = 1f;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        if (currentAmmo == -1)
            currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nestTimeToShot)
        {
            nestTimeToShot = Time.time + 1f / firerate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo--;
        //muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        { 
            Debug.Log("we hit" + hit.transform.name);

            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
            {
                health.Damage(gunDamage);
            }
            //GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
            //Destroy(effect, 0.5f);
        }
    }
}
