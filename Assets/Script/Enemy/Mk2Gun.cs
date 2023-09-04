using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mk2Gun : MonoBehaviour
{
    EnemyMK2 Mk2;
    EnemyMK2Stats Mk2Stats;

    [Header("Gun System")]
    public float fireRate = 0.4f;
    private float nextFire = 0.0f;
    public int maxAmmo = 8;
    public int ammo;
    public GameObject bulletPhase1;
    public GameObject bulletPhase2;
    public bool Fire;

    void Start()
    {
        Mk2 = GetComponentInParent<EnemyMK2>();
        Mk2Stats = GetComponentInParent<EnemyMK2Stats>();
        ammo = maxAmmo;
    }

    void Update()
    {
        if(Mk2Stats.currenthealth <= 0)
        {
            
        }
        else
        {
            if(Mk2.target != null && Time.time > nextFire)
            {
                if(ammo > 0)
                {
                    if(Mk2Stats.currenthealth <= Mk2Stats.maxHealth * 50 / 100)
                    {
                        nextFire = Time.time + fireRate;
                        FirePhase2();
                        Fire = false;
                    }
                    else
                    {
                        nextFire = Time.time + fireRate;
                        FirePhase1();
                        Fire = false;
                    }
                }
                else
                {
                    Reload();
                    Fire = false;
                }
                
            }
        }
        
    }

    private void Reload()
    {
        if(ammo <= 0)
        {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading(float reloadTime = 5f)
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
    }

    public void FirePhase1()
    {
        Instantiate(bulletPhase1, transform.position, transform.rotation);
        ammo -= 1;
        Fire = true;
    }

    public void FirePhase2()
    {
        Instantiate(bulletPhase2, transform.position, transform.rotation);
        ammo -= 1;
        Fire = true;
    }

}
