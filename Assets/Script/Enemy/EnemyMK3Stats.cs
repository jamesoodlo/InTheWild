using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMK3Stats : MonoBehaviour
{
    EnemyMK3 Mk3;
    public BaseStatData baseStatData;
    public CalculateScoreData scoreData;
    private GameObject findPlayer;
    private Inventory inven;
    private Stats stat;
    private bool getScore;

    public Slider HealthBarMk3;
    public Slider STMBarMk3;

    [Header("Enemy MK3")]
    public int maxHealth;
    public int currenthealth;
    public float maxStamina;
    public float currentStamina;
    public float staminaDrainAmount;
    public float staminaRegenerationAmount;

    [Header("Effect")]
    public GameObject sparkFx;
    public ParticleSystem hitfx;
    public GameObject electricFx;


    private void Start() 
    {
        Mk3 = GetComponent<EnemyMK3>(); 
        currenthealth = maxHealth;
        currentStamina = maxStamina;
        currenthealth = maxHealth;
        currentStamina = maxStamina;

        hitfx.Stop();
        sparkFx.SetActive(false);
        electricFx.SetActive(false);
    }

    private void Update() 
    {
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        stat = findPlayer.GetComponent<Stats>();

        if(HealthBarMk3 != null && STMBarMk3 != null)
        {
            SetCurrentHealth();
            SetCurrentStamina();
        }

        if(stat.currentHealth <= 0)
        {
            Mk3.hpBarMk3.SetActive(false);
            Mk3.stmBarMk3.SetActive(false);
        }

        if(currenthealth <= 0)
        {
            if(getScore == false)
            {
                scoreData.enemiesKill += scoreData.mk3Score;
                getScore = true;
            }  
        }
    }

    public void SetCurrentHealth()
    {
        HealthBarMk3.value = currenthealth;
        
    }

    public void SetCurrentStamina()
    {
        STMBarMk3.value = currentStamina;
    }

    public void RegenrateStamina()
    { 
        currentStamina += staminaRegenerationAmount * Time.deltaTime;
        SetCurrentStamina();
    }

    public void DrainStamina()
    { 
        currentStamina -= staminaDrainAmount * Time.deltaTime;
        SetCurrentStamina();
    }

    void OnTriggerEnter(Collider collision)
    { 
        if (collision.gameObject.tag == "Weapons")
        { 
            hitfx.Play();
            Mk3.hitSound.Play();
            if(inven.weaponSlots == 0)
            {
                currenthealth -= (inven.slotWeapons[0].dmg + baseStatData.baseATK);
                Mk3.findPlayer = GameObject.Find("Himba");
                Mk3.target = findPlayer.GetComponent<Stats>();
                Mk3.target.currentStamina += 5;
            }
            else
            {
                currenthealth -= (inven.slotWeapons[0].dmg + baseStatData.baseATK);
                Mk3.findPlayer = GameObject.Find("Himba");
                Mk3.target = findPlayer.GetComponent<Stats>();
                Mk3.target.currentStamina += 5;
            }
        }
        else
        {
            hitfx.Stop();
        }

        if (collision.gameObject.tag == "GhostDmg")
        { 
            currenthealth -= 5;
            Mk3.findPlayer = GameObject.Find("Himba");
            Mk3.target = findPlayer.GetComponent<Stats>();
        }
    }
}
