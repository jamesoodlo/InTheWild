using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    [Header("Data")]
    public CalculateScoreData calculateData;

    [Header("Upgrade Panel")]
    public GameObject canvas;
    public GameObject upgradePanel;

    [Header("Find Inventory.")]
    private GameObject findPlayer;
    private Stats statPlayer;
    private Inventory inven;
  
    [Header("Etc.")]
    public bool inRange;
    public bool canGet;

    void Start()
    {
        findPlayer = GameObject.Find("Himba");
        statPlayer = findPlayer.GetComponent<Stats>(); 
        inven = findPlayer.GetComponent<Inventory>();
        upgradePanel = GameObject.Find("UpgradePanel");
        canvas.SetActive(false);
        upgradePanel.SetActive(false);
    }

    void Update()
    {
        
        if(upgradePanel == null)
        {
            upgradePanel = GameObject.Find("UpgradePanel");
        }
        
        if(inRange == true && inven.getItem == true)
        {
            upgradePanel.SetActive(true);   
            Time.timeScale = 0;
        }
    }

    public void FinishUpgrade()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1;
        calculateData.enemiesKill = 0;
        calculateData.goldCount = 0;
        calculateData.rescueWolf = 0;
        calculateData.total = 0;
        statPlayer.currentHealth = statPlayer.maxHealth;
        statPlayer.currentStamina = statPlayer.maxStamina;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;  
            canvas.SetActive(false);      
        }
    }

    
}
