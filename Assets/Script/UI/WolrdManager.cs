using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WolrdManager : MonoBehaviour
{
    private GameObject findPlayer;
    private GameObject findUI;
    private UIManger uiManager;
    private Stats statPlayer;
    private Inventory inven;

    [Header("Data")]
    public SaveManager saveManager;
    public CalculateScoreData calculateData;
    public BaseStatData baseStatData;
    public SettingData settingData;

    [Header("UI Menu")]
    public bool menuScene;
    public GameObject newGame;
    public GameObject continueGame;
    public GameObject option;
    public GameObject quitGame;
    public GameObject settingPanel;
    public GameObject howtoplayPanel;

    [Header("Graphic")]
    public Toggle fullScreenValue;
    public TMP_Dropdown graphicValue;

    [Header("Sound")]
    public AudioSource musicAudioSource;
    public Slider musicSlider;

    private void Awake() 
    {
        saveManager.LoadGameSetting();
    }

    void Start()
    {
        findPlayer = GameObject.Find("Himba");
        findUI = GameObject.Find("UI");
    
        statPlayer = findPlayer.GetComponent<Stats>(); 
        inven = findPlayer.GetComponent<Inventory>();
        uiManager = findUI.GetComponent<UIManger>();
        settingPanel.SetActive(false);
        howtoplayPanel.SetActive(false);
        SetButton();
        SetSettingValue();
        
    }

    void Update()
    {
        SetButton();
        SetSettingValue();
        if(menuScene)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            statPlayer.HealthBar.gameObject.SetActive(false);
            statPlayer.StaminaBar.gameObject.SetActive(false);
            uiManager.goldCoinText.gameObject.SetActive(false);
            uiManager.enemiesScoreText.gameObject.SetActive(false);
            uiManager.slot1.gameObject.SetActive(false);
            uiManager.slot2.gameObject.SetActive(false);
        }
        else
        {
            statPlayer.HealthBar.gameObject.SetActive(true);
            statPlayer.StaminaBar.gameObject.SetActive(true);
            uiManager.goldCoinText.gameObject.SetActive(true);
            uiManager.enemiesScoreText.gameObject.SetActive(true);
            uiManager.slot1.gameObject.SetActive(true); 
        } 
    }

    public void SetSettingValue()
    {
        //Graphic
        graphicValue.value = settingData.qualityIndex;
        //PlayerPrefs.SetInt("QualityValue", settingData.qualityIndex);

        //Display
        fullScreenValue.isOn = settingData.isFullScreen;
        //PlayerPrefs.SetInt("DisplayValue", (settingData.isFullScreen ? 1 : 0));

        //MusicSound
        musicSlider.value = settingData.musicSound;
        musicAudioSource.volume = settingData.musicSound;
        //PlayerPrefs.SetFloat("VolumeValue", settingData.musicSound);
    }

#region MainManuFunction
    public void SetButton()
    {
        if(settingData.isFirstPlay)
        {
            continueGame.SetActive(false);
        }
        else
        {
            continueGame.SetActive(true);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Single);
        Time.timeScale = 1;
        settingData.isFirstPlay = false;

        calculateData.goldCoinsPocket = 0;
        baseStatData.baseHP = baseStatData.stdHP;
        baseStatData.baseSTM = baseStatData.stdSTM;
        baseStatData.baseSTMRe = baseStatData.stdSTMRe;
        baseStatData.baseATK = baseStatData.stdATK;
        baseStatData.baseSPD = baseStatData.stdSPD;

        baseStatData.upgradeHPCost = 15;
        baseStatData.upgradeSTMCost = 15;
        baseStatData.upgradeSTMReCost = 20;
        baseStatData.upgradeATKCost = 25;
        baseStatData.upgradeSPDCost = 30;

        statPlayer.HealthBar.gameObject.SetActive(true);
        statPlayer.StaminaBar.gameObject.SetActive(true);
        uiManager.goldCoinText.gameObject.SetActive(true);
        uiManager.enemiesScoreText.gameObject.SetActive(true);
        uiManager.slot1.gameObject.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Single);
        saveManager.LoadGame();
        Time.timeScale = 1;

        statPlayer.HealthBar.gameObject.SetActive(true);
        statPlayer.StaminaBar.gameObject.SetActive(true);
        uiManager.goldCoinText.gameObject.SetActive(true);
        uiManager.enemiesScoreText.gameObject.SetActive(true);
        uiManager.slot1.gameObject.SetActive(true);
    }
    
    public void Option()
    {
        settingPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void FinishSettingMenu()
    {
        musicSlider.value = settingData.musicSound;
        graphicValue.value = settingData.qualityIndex;
        fullScreenValue.isOn = settingData.isFullScreen;
        saveManager.SaveGame();
        settingPanel.SetActive(false);
    }

#endregion

#region SettingFunction
    public void SetMusicSound()
    {
        settingData.musicSound = musicSlider.value;
        musicAudioSource.volume = settingData.musicSound;
    }

    public void SetQuality(int qualityIndex)
    {
        qualityIndex = graphicValue.value;
        settingData.qualityIndex = graphicValue.value;
        QualitySettings.SetQualityLevel(qualityIndex);  
    }

    public void SetFullscreen(bool isFullScreen)
    {
        isFullScreen = fullScreenValue.isOn;
        settingData.isFullScreen = fullScreenValue.isOn;
        Screen.fullScreen = isFullScreen;   
    }
#endregion

#region InGameFunction
    public void Lobby()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        saveManager.SaveGame();
        SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Single);
    }

    public void SettingPanel()
    {
        howtoplayPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void FinishSetting()
    {
        musicSlider.value = settingData.musicSound;
        graphicValue.value = settingData.qualityIndex;
        fullScreenValue.isOn = settingData.isFullScreen;
        saveManager.SaveGame();
        settingPanel.SetActive(false);
    }

    public void HowToPlay()
    {
        howtoplayPanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        saveManager.SaveGame();
        settingPanel.SetActive(false);
    }

    public void Quit()
    {
        saveManager.SaveGame();
        Application.Quit();
    }
#endregion

}
