using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{
    private GameObject findPlayer;
    private Stats statPlayer;
    private Inventory invenPlayer;
    private GameObject findHPSlider;
    private GameObject findSTMSlider;
    private GameObject findSTMReSlider;
    private GameObject findATKSlider;
    private GameObject findSPDSlider;

    [Header("UI GameObject")]
    public GameObject upgradePanel;
    public GameObject pausePanel;
    public Slider HPSlider;
    public Slider STMSlider;
    public Slider STMReSlider;
    public Slider ATKSlider;
    public Slider SPDSlider;

    [Header("Text Cost GameObject")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI HPCostText;
    public TextMeshProUGUI STMCostText;
    public TextMeshProUGUI STMReCostText;
    public TextMeshProUGUI ATKCostText;
    public TextMeshProUGUI SPDCostText;

    [Header("Base Stats")]
    public SaveManager saveManager;
    public BaseStatData baseStat;
    public CalculateScoreData scoreData;
    public float baseHP;
    public float baseSTM;
    public float baseSTMRe;
    public int baseATK;
    public float baseSPD;

    [Header("Upgrade Cost")]
    public int upgradeHPCost;
    public int upgradeSTMCost;
    public int upgradeSTMReCost;
    public int upgradeATKCost;
    public int upgradeSPDCost;

    void Start()
    {
        baseHP = baseStat.baseHP;
        baseSTM = baseStat.baseSTM;
        baseSTMRe = baseStat.baseSTMRe;
        baseATK = baseStat.baseATK;
        baseSPD = baseStat.baseSPD;


        upgradePanel = GameObject.Find("UpgradePanel");

        findHPSlider = GameObject.Find("HPUpgrade");
        HPSlider = findHPSlider.GetComponent<Slider>();

        findSTMSlider = GameObject.Find("STMUpgrade");
        STMSlider = findSTMSlider.GetComponent<Slider>();

        findSTMReSlider = GameObject.Find("STMReUpgrade");
        STMReSlider = findSTMReSlider.GetComponent<Slider>();

        findATKSlider = GameObject.Find("ATKUpgrade");
        ATKSlider = findATKSlider.GetComponent<Slider>();

        findSPDSlider = GameObject.Find("SPDUpgrade");
        SPDSlider = findSPDSlider.GetComponent<Slider>();

        findPlayer = GameObject.Find("Himba");
        statPlayer = findPlayer.GetComponent<Stats>(); 
        invenPlayer = findPlayer.GetComponent<Inventory>();
    }

    
    void Update()
    {
        upgradePanel = GameObject.Find("UpgradePanel");

        findHPSlider = GameObject.Find("HPUpgrade");
        HPSlider = findHPSlider.GetComponent<Slider>();

        findSTMSlider = GameObject.Find("STMUpgrade");
        STMSlider = findSTMSlider.GetComponent<Slider>();

        findSTMReSlider = GameObject.Find("STMReUpgrade");
        STMReSlider = findSTMReSlider.GetComponent<Slider>();

        findATKSlider = GameObject.Find("ATKUpgrade");
        ATKSlider = findATKSlider.GetComponent<Slider>();

        findSPDSlider = GameObject.Find("SPDUpgrade");
        SPDSlider = findSPDSlider.GetComponent<Slider>();
        goldText.text = invenPlayer.goldCoinsPocket.ToString();
        SetBaseStatsValue();
        SetCost();
        baseHP = baseStat.baseHP;
        baseSTM = baseStat.baseSTM;
        baseSTMRe = baseStat.baseSTMRe;
        baseATK = baseStat.baseATK;
        baseSPD = baseStat.baseSPD;
    }


    private void SetBaseStatsValue()
    {
        HPSlider.value = baseHP;
        STMSlider.value = baseSTM;
        STMReSlider.value = baseSTMRe;
        ATKSlider.value = baseATK;
        SPDSlider.value = baseSPD;
    }


    private void SetCost()
    {
        upgradeHPCost = baseStat.upgradeHPCost;
        upgradeSTMCost = baseStat.upgradeSTMCost;
        upgradeSTMReCost = baseStat.upgradeSTMReCost;
        upgradeATKCost = baseStat.upgradeATKCost;
        upgradeSPDCost = baseStat.upgradeSPDCost;

        HPCostText.text = upgradeHPCost.ToString();
        STMCostText.text = upgradeSTMCost.ToString();
        STMReCostText.text = upgradeSTMReCost.ToString();
        ATKCostText.text = upgradeATKCost.ToString();
        SPDCostText.text = upgradeSPDCost.ToString();
    }

#region Button Function
    public void HPUpgrade()
    {
        if(HPSlider.value != HPSlider.maxValue)
        {
            if(invenPlayer.goldCoinsPocket >= upgradeHPCost)
            {
                invenPlayer.goldCoinsPocket -= upgradeHPCost;
                baseStat.baseHP += 20;
                baseStat.upgradeHPCost += 5;
            }
            else
            {
            
            }
        }
        else
        {
            
        }
    }

    public void STMUpgrade()
    {
        if(STMSlider.value != STMSlider.maxValue)
        {
            if(scoreData.goldCoinsPocket >= upgradeSTMCost)
            {
                scoreData.goldCoinsPocket -= upgradeSTMCost;
                baseStat.baseSTM += 20;
                baseStat.upgradeSTMCost += 5;
            }
            else
            {
                
            }
        }
        else
        {
            
        } 
    }

    public void STMReUpgrade()
    {
        if(STMReSlider.value != STMReSlider.maxValue)
        {
            if(scoreData.goldCoinsPocket >= upgradeSTMReCost)
            {
                scoreData.goldCoinsPocket -= upgradeSTMReCost;
                baseStat.baseSTMRe += 5;
                baseStat.upgradeSTMReCost += 5;
            }
            else
            {
                
            } 
        }
        else
        {

        }
    }

    public void ATKUpgrade()
    {
        if(ATKSlider.value != ATKSlider.maxValue)
        {
            if(scoreData.goldCoinsPocket >= upgradeATKCost)
            {
                scoreData.goldCoinsPocket -= upgradeATKCost;
                baseStat.baseATK += 2;
                baseStat.upgradeATKCost += 5;
            }
            else
            {

            }
        }
        else
        {

        }
    }

    public void SPDUpgrade()
    {
        if(SPDSlider.value != SPDSlider.maxValue)
        {
            if(scoreData.goldCoinsPocket >= upgradeATKCost)
            {
                scoreData.goldCoinsPocket -= upgradeSPDCost;
                baseStat.baseSPD += 1.0f;
                baseStat.upgradeSPDCost += 5;
            }
            else
            {
                
            }
        }
    }

    public void FinishUpgrade()
    {
        upgradePanel.SetActive(false);
        saveManager.SaveGame();
        statPlayer.currentHealth = statPlayer.maxHealth;
        statPlayer.currentStamina = statPlayer.maxStamina;
    }
#endregion
}
