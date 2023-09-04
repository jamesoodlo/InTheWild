using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class weapon : MonoBehaviour
{
    Animator anim;

    [Header("Weapons")]
    public bool isDagger;
    public bool isAxe;
    public bool isBlade;
    public bool isSpear;
    public bool isHammer;
    public bool isLaser;

    [Header("Weapons Stats")]
    public int dmg;
    public int HvyDmg;
    public int staminaCost;
    public int HvySTMCost;
    public Collider dmgCollider;

    [Header("Skill")]
    public float CdSkill;
    public GameObject skillFx;

    [Header("Find Inventory")]
    public GameObject findPlayer;
    public Stats stat;
    public Inventory inven;

    [Header("Find EnemyStats")]
    public GameObject findEnemy;
    public EnemyStats enemyStats;

    void Start() 
    {
        dmgCollider.enabled = false;
        skillFx.SetActive(false);
    }

    void Update() 
    {
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        stat = findPlayer.GetComponent<Stats>();
        anim = findPlayer.GetComponent<Animator>();

        findEnemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyStats = findEnemy.GetComponent<EnemyStats>();

        if(findEnemy == null && enemyStats == null)
        {
            Debug.Log("can't find Enemy");
        }
        
        damageWeapon();      
    }

    public void EnabledDamageCollider()
    {
        dmgCollider.enabled = true;
    }

    public void DisabledDamageCollider()
    {
        dmgCollider.enabled = false;
    }

    private void damageWeapon()
    {
        if(isAxe)
        {
            dmg = 20;
            HvyDmg = 25;
        }
        else if(isBlade)
        {
            dmg = 10;
            HvyDmg = 15;
        }
        else if(isSpear)
        {
            dmg = 7;
            HvyDmg = 12;
        }
        else if(isHammer)
        {
            dmg = 25;
            HvyDmg = 30;
        }
        else if(isLaser)
        {
            dmg = 15;
            HvyDmg = 20;
        }
    }

}
