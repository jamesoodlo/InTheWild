using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    public bool onHit;
    private GameObject findPlayer;
    private Inventory inven;
    private Stats stat;
    private bool getScore;
    public AudioSource hitSfx;
    public AudioSource sparkSfx;
    public AudioSource dieSfx;

    public CalculateScoreData calculateData;

    [Header("Stats")]
    public GameObject serverHPBar;
    public Slider serverHPSlider;
    public int maxHealth;
    public int currenthealth;

    [Header("Spawn Phase")]
    public bool spawnPhase;

    [Header("Spawn System")]
    public int enemyCount;
    public GameObject enemy;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;

    [Header("Effect")]
    public ParticleSystem hitfx;
    public GameObject[] fireFx;
    public GameObject SmokeVfx;
    public GameObject ElectricVfx;
    public GameObject SparkVfx;

    void Start()
    {
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        stat = findPlayer.GetComponent<Stats>(); 

        currenthealth = maxHealth;
        serverHPBar.SetActive(false);
        hitfx.Stop();
        SmokeVfx.SetActive(false);
        SparkVfx.SetActive(false);
        ElectricVfx.SetActive(false);
        fireFx[0].SetActive(false);
        fireFx[1].SetActive(false);
        fireFx[2].SetActive(false);
    }

    void Update()
    {
        SetPhase();
        if(serverHPSlider != null)
        {
            SetcurrentHealth();
        }

        if(currenthealth <=  maxHealth * 50 / 100)
        {
            sparkSfx.Play();
            fireFx[0].SetActive(true);
            fireFx[1].SetActive(true);
            fireFx[2].SetActive(true);
        }

        if(stat.currentHealth <= 0)
        {
            serverHPBar.SetActive(false);
        }

        if(currenthealth <= 0)
        {
            serverHPBar.SetActive(false);
            onHit = false;
            if(getScore == false)
            {
                calculateData.serverDestroy += calculateData.serverScore;
                getScore = true;
            } 
        }
    }

    public void SetcurrentHealth()
    {
        serverHPSlider.value = currenthealth;
        
    }

    private void SetPhase()
    {
        if(currenthealth < 650 && currenthealth > 600)
        {
            spawnPhase = true;
            ElectricVfx.SetActive(true);
            while(enemyCount < 1)
            {
                Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                Instantiate(enemy, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                Instantiate(enemy, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                Instantiate(enemy, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                enemyCount += 1;  
            }

            StartCoroutine(phaseFalse());
        }
        else if(currenthealth < 500 && currenthealth > 450)
        {
            spawnPhase = true;
            SmokeVfx.SetActive(true); 
            
            while(enemyCount < 2)
            {
                Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                Instantiate(enemy, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                Instantiate(enemy, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                Instantiate(enemy, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                enemyCount += 1;  
            }
              
            StartCoroutine(phaseFalse());
        }
        else if(currenthealth < 300 && currenthealth > 250)
        {
            spawnPhase = true;
            SmokeVfx.SetActive(true); 
            
            while(enemyCount < 3)
            {
                Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                Instantiate(enemy, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                Instantiate(enemy, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                Instantiate(enemy, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                enemyCount += 1;  
            }
              
            StartCoroutine(phaseFalse());
        }
        else if(currenthealth < 200)
        {
            spawnPhase = true;
            SmokeVfx.SetActive(true); 
            
            while(enemyCount < 4)
            {
                Instantiate(enemy, spawnPoint1.transform.position, spawnPoint1.transform.rotation);
                Instantiate(enemy, spawnPoint2.transform.position, spawnPoint2.transform.rotation);
                Instantiate(enemy, spawnPoint3.transform.position, spawnPoint3.transform.rotation);
                Instantiate(enemy, spawnPoint4.transform.position, spawnPoint4.transform.rotation);
                enemyCount += 1;  
            }
              
            StartCoroutine(phaseFalse());
        }
    }

    private IEnumerator phaseFalse()
    {
        yield return new WaitForSeconds(1f);
        spawnPhase = false;     
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapons")
        { 
            onHit = true;
            hitSfx.Play();
            hitfx.Play();
            serverHPBar.SetActive(true);
            if(inven.weaponSlots == 0)
            {
                currenthealth -= inven.slotWeapons[0].dmg;
            }
            else
            {
                currenthealth -= inven.slotWeapons[1].dmg;
            }
        }
        else
        {
            hitSfx.Stop();
            hitfx.Stop();
        }
    }
}
