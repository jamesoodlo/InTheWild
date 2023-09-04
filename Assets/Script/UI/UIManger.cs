using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManger : MonoBehaviour
{
    private GameObject findMk2;
    private GameObject findMk3;
    private GameObject findServer;

    [Header("Data")]
    public CalculateScoreData scoreData;

    [Header("Enemies Object")]
    public EnemyMK2 Mk2;
    public EnemyMK3 Mk3;
    public Server server;

    [Header("HUD Slot 1")]
    public GameObject slot1;
    public GameObject[] slot1Icon;

    [Header("HUD Slot 2")]
    public GameObject slot2;
    public GameObject[] slot2Icon;

    [Header("HUD Text")]
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI currentStaminaText;
    public TextMeshProUGUI maxStaminaText;

    [Header("UI Text")]
    public TextMeshProUGUI goldCoinText;
    public TextMeshProUGUI enemiesScoreText;

    [Header("GetConponent Inventory")]
    private GameObject findPlayer;
    public Inventory inven;
    public Stats stat;


    void Start()
    {
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
    }

    void Update()
    {   
        stat = findPlayer.GetComponent<Stats>();
        SetText();
        SetIconSlot();
    }

    private void SetText()
    {
        goldCoinText.text = scoreData.goldCount.ToString();
        enemiesScoreText.text = scoreData.enemiesKill.ToString();
        currentHealthText.text = stat.currentHealth.ToString("F0");
        maxHealthText.text = stat.maxHealth.ToString();
        currentStaminaText.text = stat.currentStamina.ToString("F0");
        maxStaminaText.text = stat.maxStamina.ToString();
    }

    private void SetIconSlot()
    {
        if(inven.slotWeapons[0].isAxe)
        {
            slot1Icon[0].SetActive(true);
            slot1Icon[1].SetActive(false);
            slot1Icon[2].SetActive(false);
            slot1Icon[3].SetActive(false);
            slot1Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[0].isBlade)
        {
            slot1Icon[0].SetActive(false);
            slot1Icon[1].SetActive(true);
            slot1Icon[2].SetActive(false);
            slot1Icon[3].SetActive(false);
            slot1Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[0].isSpear)
        {
            slot1Icon[0].SetActive(false);
            slot1Icon[1].SetActive(false);
            slot1Icon[2].SetActive(true);
            slot1Icon[3].SetActive(false);
            slot1Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[0].isHammer)
        {
            slot1Icon[0].SetActive(false);
            slot1Icon[1].SetActive(false);
            slot1Icon[2].SetActive(false);
            slot1Icon[3].SetActive(true);
            slot1Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[0].isLaser)
        {
            slot1Icon[0].SetActive(false);
            slot1Icon[1].SetActive(false);
            slot1Icon[2].SetActive(false);
            slot1Icon[3].SetActive(false);
            slot1Icon[4].SetActive(true);
        }

        if(inven.slotWeapons[1] == null)
        {
            slot2Icon[0].SetActive(false);
            slot2Icon[1].SetActive(false);
            slot2Icon[2].SetActive(false);
            slot2Icon[3].SetActive(false);
            slot2Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[1].isAxe)
        {
            slot2Icon[0].SetActive(true);
            slot2Icon[1].SetActive(false);
            slot2Icon[2].SetActive(false);
            slot2Icon[3].SetActive(false);
            slot2Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[1].isBlade)
        {
            slot2Icon[0].SetActive(false);
            slot2Icon[1].SetActive(true);
            slot2Icon[2].SetActive(false);
            slot2Icon[3].SetActive(false);
            slot2Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[1].isSpear)
        {
            slot2Icon[0].SetActive(false);
            slot2Icon[1].SetActive(false);
            slot2Icon[2].SetActive(true);
            slot2Icon[3].SetActive(false);
            slot2Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[1].isHammer)
        {
            slot2Icon[0].SetActive(false);
            slot2Icon[1].SetActive(false);
            slot2Icon[2].SetActive(false);
            slot2Icon[3].SetActive(true);
            slot2Icon[4].SetActive(false);
        }
        else if(inven.slotWeapons[1].isLaser)
        {
            slot2Icon[0].SetActive(false);
            slot2Icon[1].SetActive(false);
            slot2Icon[2].SetActive(false);
            slot2Icon[3].SetActive(false);
            slot2Icon[4].SetActive(true);
        }
    }
}
