using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    Inventory inven;
    private float dmgEnemy;
    private bool getScore;

    [Header("Player Stats")]
    public BaseStatData baseStat;
    public float maxHealth;
    public float currentHealth;

    public float maxStamina;
    public float currentStamina;
    public float staminaRegenerationAmount;
    public float staminaRegenTimer = 0;
    public float defArmor;
    public float speed;

    public int attackDamage;

    public bool hasGhost;
    public GameObject ghostIcon;
    public bool isDead;

    [Header("UI Stats")]
    private GameObject findHPBar;
    private GameObject findSTMBar;
    public Slider HealthBar;
    public Slider StaminaBar;

    private void Awake() 
    {
        //findHPBar = GameObject.Find("HPBar");
        //findSTMBar = GameObject.Find("STMBar");
    }

    private void Start() 
    {
        inven = GetComponent<Inventory>();
        HealthBar = findHPBar.GetComponent<Slider>();
        StaminaBar = findSTMBar.GetComponent<Slider>();

        maxHealth = baseStat.baseHP;
        maxStamina = baseStat.baseSTM;
        staminaRegenerationAmount = baseStat.baseSTMRe;
        speed = baseStat.baseSPD;

        currentHealth = maxHealth;
        currentStamina = maxStamina;

        HealthBar.maxValue = maxHealth;
        StaminaBar.maxValue = maxStamina;
    }

    private void Update() 
    {
        maxHealth = baseStat.baseHP;
        maxStamina = baseStat.baseSTM;
        staminaRegenerationAmount = baseStat.baseSTMRe;
        speed = baseStat.baseSPD;
        attackDamage = baseStat.baseATK;

        HealthBar.maxValue = baseStat.baseHP;
        StaminaBar.maxValue = baseStat.baseSTM;
        
        SetCurrentHealth();
        SetCurrentStamina();
        RegenrateStamina();
        if(currentHealth <= 0)
        {
            //SceneManager.LoadScene("Lobby");
            inven.slotWeapons[1] = null;
            hasGhost = false;
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        if(currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }

        if(currentStamina < 0)
        {
            currentStamina = 0;
        }
        

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        

        if(hasGhost)
        {
            if(!getScore)
            {
                inven.calculateData.rescueWolf += 30;
                getScore = true;
            }
            ghostIcon.SetActive(true);
        }
        else
        {
            inven.calculateData.rescueWolf = 0;
            ghostIcon.SetActive(false);
        }
    }

#region PlayerStat

    public void SetCurrentHealth()
    {
        HealthBar.value = currentHealth;
    }

    public void SetCurrentStamina()
    {
        StaminaBar.value = currentStamina;
    }

    public void RegenrateStamina()
    { 
        staminaRegenTimer += Time.deltaTime;

        if(currentStamina < maxStamina && staminaRegenTimer > 1f)
        {
            currentStamina += staminaRegenerationAmount * Time.deltaTime;
            SetCurrentStamina();
        }
    }

    public void TakeStamina(float staminaCost)
    { 
        currentStamina = currentStamina - staminaCost;
        SetCurrentStamina();
    }
#endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WeaponsEnemy")
        {
            currentHealth -= dmgEnemy;
        }

        if (other.gameObject.tag == "Bullet")
        {
            currentHealth -= 13;
        }

        if (other.gameObject.tag == "Mk3Dmg")
        {
            currentHealth -= 25;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Fire")
        {
            currentHealth -= 5 * Time.deltaTime;
        }

        if (other.gameObject.tag == "Lobby")
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;
        }

        if (other.gameObject.tag == "Lv1")
        {
            dmgEnemy = 5;
        }
        else if (other.gameObject.tag == "Lv2")
        {
            dmgEnemy = 8;
        }
        else if (other.gameObject.tag == "Lv3")
        {
            dmgEnemy = 12;
        }
        else if (other.gameObject.tag == "Lv4")
        {
            dmgEnemy = 17;
        }
        else if (other.gameObject.tag == "Lv5")
        {
            dmgEnemy = 22;
        }
        else if (other.gameObject.tag == "Lv6")
        {
            dmgEnemy = 30;
        }
    }
    
}
