using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMK2Stats : MonoBehaviour
{
    EnemyMK2 Mk2;
    public BaseStatData baseStatData;
    public CalculateScoreData scoreData;
    private GameObject findPlayer;
    private Inventory inven;
    private Stats stat;
    private bool getScore;

    public Slider HealthBarMk2;

    [Header("Effect")]
    public GameObject sparkFx;
    public ParticleSystem hitfx;
    public GameObject[] fireFx;

    [Header("Enemy MK2")]
    public int maxHealth;
    public int currenthealth;

    private void Start() 
    {

        Mk2 = GetComponent<EnemyMK2>(); 
        currenthealth = maxHealth;
        currenthealth = maxHealth;

        sparkFx.SetActive(false);
        hitfx.Stop();
        fireFx[0].SetActive(false);
        fireFx[1].SetActive(false);
        fireFx[2].SetActive(false);
    }

    private void Update() 
    {

        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        stat = findPlayer.GetComponent<Stats>();

        if(HealthBarMk2 != null)
        {
            SetCurrentHealth();
        }

        if(stat.currentHealth <= 0)
        {
            Mk2.healthBarMk2.SetActive(false);
        }

        if(currenthealth <= 0)
        {
            if(getScore == false)
            {
                scoreData.enemiesKill += scoreData.mk2Score;
                getScore = true;
            } 
        }

        SetSpark();
    }

    private void SetSpark()
    {
        if(currenthealth <= maxHealth * 50/100 )
        {
            sparkFx.SetActive(true);
            Mk2.sparkSound.Play();
            fireFx[0].SetActive(true);
            fireFx[1].SetActive(true);
            fireFx[2].SetActive(true);
        }
    }

    public void SetCurrentHealth()
    {
        HealthBarMk2.value = currenthealth;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapons")
        { 
            hitfx.Play();
            Mk2.hitSound.Play();
            stat.currentStamina += 5;
            if(inven.weaponSlots == 0)
            {
                currenthealth -= (inven.slotWeapons[0].dmg + baseStatData.baseATK);
            }
            else
            {
                currenthealth -= (inven.slotWeapons[1].dmg + baseStatData.baseATK);
            }

            
        }
        else
        {
            hitfx.Stop();
        }

        if (collision.gameObject.tag == "GhostDmg")
        { 
            currenthealth -= 5;
        }
    }
}
