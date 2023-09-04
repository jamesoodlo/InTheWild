using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public BaseStatData baseStatData;
    public CalculateScoreData calculateData;
    public SettingData settingData;
   
    public SaveData saveData;

    private void Update() 
    {
        saveData.baseHP = baseStatData.baseHP;
        saveData.baseSTM = baseStatData.baseSTM;
        saveData.baseSTMRe = baseStatData.baseSTMRe;
        saveData.baseATK = baseStatData.baseATK;
        saveData.baseSPD = baseStatData.baseSPD;

        saveData.upgradeHPCost = baseStatData.upgradeHPCost;
        saveData.upgradeSTMCost = baseStatData.upgradeSTMCost;
        saveData.upgradeSTMReCost = baseStatData.upgradeSTMReCost;
        saveData.upgradeATKCost = baseStatData.upgradeATKCost;
        saveData.upgradeSPDCost = baseStatData.upgradeSPDCost;

        saveData.goldCoinsPocket = calculateData.goldCoinsPocket;

        saveData.musicSound = settingData.musicSound;
        saveData.qualityIndex = settingData.qualityIndex;
        saveData.isFullScreen = settingData.isFullScreen;
        saveData.isFirstPlay = settingData.isFirstPlay;
    }

    public void SaveGame()
    {
        //BinaryFormatter bf = new BinaryFormatter(); 
        //FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); 
        SaveData data = new SaveData();

        data.baseHP = baseStatData.baseHP;
        data.baseSTM = baseStatData.baseSTM;
        data.baseSTMRe = baseStatData.baseSTMRe;
        data.baseATK = baseStatData.baseATK;
        data.baseSPD = baseStatData.baseSPD;

        data.upgradeHPCost = baseStatData.upgradeHPCost;
        data.upgradeSTMCost = baseStatData.upgradeSTMCost;
        data.upgradeSTMReCost = baseStatData.upgradeSTMReCost;
        data.upgradeATKCost = baseStatData.upgradeATKCost;
        data.upgradeSPDCost = baseStatData.upgradeSPDCost;

        data.goldCoinsPocket = calculateData.goldCoinsPocket;

        data.musicSound = settingData.musicSound;
        data.qualityIndex = settingData.qualityIndex;
        data.isFullScreen = settingData.isFullScreen;
        data.isFirstPlay = settingData.isFirstPlay;

        string saveDataString = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("Save", saveDataString);
        PlayerPrefs.Save();

        //bf.Serialize(file, data);
        //file.Close();
        Debug.Log("Game data saved!");
        
    }

    public void LoadGame()
    {
        string loadDataString = PlayerPrefs.GetString("Save");   
        SaveData loadSave = JsonUtility.FromJson<SaveData>(loadDataString);

        if (loadSave != null)
        {
            //File.Exists(Application.persistentDataPath + "/MySaveData.dat") //in IF
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            //SaveData data = (SaveData)bf.Deserialize(file);
            //file.Close();
            

            baseStatData.baseHP = loadSave.baseHP;
            baseStatData.baseSTM = loadSave.baseSTM;
            baseStatData.baseSTMRe = loadSave.baseSTMRe;
            baseStatData.baseATK = loadSave.baseATK;
            baseStatData.baseSPD = loadSave.baseSPD;

            baseStatData.upgradeHPCost = loadSave.upgradeHPCost;
            baseStatData.upgradeSTMCost = loadSave.upgradeSTMCost;
            baseStatData.upgradeSTMReCost = loadSave.upgradeSTMReCost;
            baseStatData.upgradeATKCost = loadSave.upgradeATKCost;
            baseStatData.upgradeSPDCost = loadSave.upgradeSPDCost;

            calculateData.goldCoinsPocket = loadSave.goldCoinsPocket;

            settingData.musicSound = loadSave.musicSound;
            settingData.qualityIndex = loadSave.qualityIndex;
            settingData.isFullScreen = loadSave.isFullScreen;
            settingData.isFirstPlay = loadSave.isFirstPlay;

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

     public void LoadGameSetting()
    {
        string loadDataString = PlayerPrefs.GetString("Save");   
        SaveData loadSave = JsonUtility.FromJson<SaveData>(loadDataString);

        if (loadSave != null)
        {
            settingData.musicSound = loadSave.musicSound;
            settingData.qualityIndex = loadSave.qualityIndex;
            settingData.isFullScreen = loadSave.isFullScreen;
            settingData.isFirstPlay = loadSave.isFirstPlay;

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }
}

[Serializable]
public class SaveData
{
    [Header("BaseStats")]
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

    [Header("Gold Pocket")]
    public int goldCoinsPocket;

    [Header("All Setting")]
    public float musicSound;
    public int qualityIndex;
    public bool isFullScreen;

    [Header("Menu Setting")]
    public bool isFirstPlay;
}







