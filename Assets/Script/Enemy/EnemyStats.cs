using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    Enemy Mk1;
    EnemyMK2 Mk2;
    EnemyMK2Stats mk2Stat;
    EnemyMK3 Mk3;
    EnemyMK3Stats mk3Stat;

    private GameObject findPlayer;
    private Inventory inven;
    private Stats stat;

    [Header("Data")]
    public BaseStatData baseStatData;
    public CalculateScoreData scoreData;

    [Header("Enemies Type")]
    public bool Enemy1;
    public bool Enemy2;
    public bool Enemy3;
    
    [Header("Effect")]
    public ParticleSystem hitfx;
    public GameObject sparkFx;

    [Header("Stats")]
    public int maxHealth;
    public int currenthealth;

    private void Start() 
    {
        Mk1 = GetComponent<Enemy>();
        if(Enemy1)
        {
            currenthealth = maxHealth;
            StartCoroutine(SetDelayHP());
        }
        hitfx.Stop();
        sparkFx.SetActive(false);
    }

    private void Update() 
    {
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        stat = findPlayer.GetComponent<Stats>();

        if(Enemy1)
        {
            SetSpark();
            if(currenthealth <= 0)
            {
                
            }
        }

        if(Enemy2)
        {
            mk2Stat = GetComponent<EnemyMK2Stats>();
            currenthealth = mk2Stat.currenthealth;
        }

        if(Enemy3)
        {
            mk3Stat = GetComponent<EnemyMK3Stats>();
            currenthealth = mk3Stat.currenthealth;
        }
    }

    private void SetSpark()
    {
        if(currenthealth <= maxHealth * 55/100 )
        {
            sparkFx.SetActive(true);
            Mk1.sparkSound.Play();
        }
    }

    private IEnumerator SetDelayHP()
    {
        yield return new WaitForSeconds(0.1f);
        currenthealth = maxHealth;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapons")
        { 
            if(Enemy1)
            {
                Mk1.GetHurt();
                Mk1.hitSound.Play();
                if(inven.weaponSlots == 0)
                {
                    currenthealth -= (inven.slotWeapons[0].dmg + baseStatData.baseATK);
                    stat.currentStamina += 5;
                    hitfx.Play();
                    Mk1.findPlayer = GameObject.Find("Himba");
                    Mk1.target = findPlayer.GetComponent<Stats>();
                }
                else
                {
                    currenthealth -= (inven.slotWeapons[0].dmg + baseStatData.baseATK);
                    stat.currentStamina += 5;
                    hitfx.Play();
                    Mk1.findPlayer = GameObject.Find("Himba");
                    Mk1.target = findPlayer.GetComponent<Stats>();
                }
                
            }
            
        }
        else
        {
            hitfx.Stop();
        }

        if (collision.gameObject.tag == "GhostDmg")
        { 
            if(Enemy1)
            { 
                currenthealth -= 5;
                Mk1.findPlayer = GameObject.Find("Himba");
                Mk1.target = findPlayer.GetComponent<Stats>();
            }
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Lv1")
        {
            if(Enemy1)
            {
                maxHealth = 100;
            }
        }
        else if (other.gameObject.tag == "Lv2")
        {
            if(Enemy1)
            {
                maxHealth = 120;
            }
        }
        else if (other.gameObject.tag == "Lv3")
        {
            if(Enemy1)
            {
                maxHealth = 140;
            }
        }
        else if (other.gameObject.tag == "Lv4")
        {
            if(Enemy1)
            {
                maxHealth = 160;
            }
        }
        else if (other.gameObject.tag == "Lv5")
        {
            if(Enemy1)
            {
                maxHealth = 200;
            }
        }
        else if (other.gameObject.tag == "Lv6")
        {
            if(Enemy1)
            {
                maxHealth = 250;
            }
        }
    }
}
