using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private GameObject findPlayer;
    private Stats statPlayer;
    private Inventory inven;

    public int enemiesCount;
    public int[] bossCount;
    private EnemyMK2[] mk2;
    private EnemyMK3[] mk3;
    private Enemy[] enemy;

    public InputAction escape;
    public bool escapeButton;

    [Header("Data")]
    public SaveManager saveManager;
    public CalculateScoreData calculateData;
    public BaseStatData baseStatData;
    public SettingData settingData;

    [Header("UI In Game")]
    public bool lobby; 
    public GameObject pausePanel;
    public GameObject pauseInGamePanel;
    public GameObject calculatePanel;
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public GameObject settingPanel;
    public GameObject howtoplayPanel;

    [Header("Graphic")]
    public Toggle fullScreenValue;
    public TMP_Dropdown graphicValue;

    [Header("Sound")]
    public GameObject musicSfxObject;
    public GameObject battleSfxObject;
    public GameObject battleBossSfxObject;
    public AudioSource musicSfx;
    public AudioSource battleSfx;
    public AudioSource battleBossSfx;
    public Slider musicSlider;

    void Awake() 
    {
        escape.started += context => escapeButton = true;
        escape.performed += context => escapeButton = true;
        escape.canceled += context => escapeButton = false;
    }

    void Start()
    {

        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        pausePanel.SetActive(false);
        pauseInGamePanel.SetActive(false);
        calculatePanel.SetActive(false);
        settingPanel.SetActive(false);
        howtoplayPanel.SetActive(false);

        SetSettingValue();
        battleSfxObject.SetActive(false);
    }

    void Update()
    {
        findPlayer = GameObject.Find("Himba");
        statPlayer = findPlayer.GetComponent<Stats>(); 
        inven = findPlayer.GetComponent<Inventory>();

        checkNumEnemies();
        checkEnemies();
        SetCursor();

        if(escapeButton)
        {
            Time.timeScale = 0;
            if(lobby)
            {
                pausePanel.SetActive(true);
            }
            else
            {
                pauseInGamePanel.SetActive(true);
            }
        }
    }

    public void SetSettingValue()
    {
        //Graphic
        graphicValue.value = settingData.qualityIndex;

        //Display
        fullScreenValue.isOn = settingData.isFullScreen;

        //MusicSound
        musicSlider.value = settingData.musicSound;
        musicSfx.volume = settingData.musicSound;
        battleSfx.volume = settingData.musicSound;
        battleBossSfx.volume = settingData.musicSound;
    }

    private void checkNumEnemies()
    {
        enemy = FindObjectsOfType<Enemy>();
        enemiesCount = FindObjectsOfType<Enemy>().Length;

        mk2 = FindObjectsOfType<EnemyMK2>();
        bossCount[0] = FindObjectsOfType<EnemyMK2>().Length;

        mk3 = FindObjectsOfType<EnemyMK3>();
        bossCount[1] = FindObjectsOfType<EnemyMK3>().Length;
    }

    private void checkEnemies()
    {
        if(bossCount[0] > 0 && bossCount[1] <= 0) 
        {
            enemiesCount += 301;
        }
        else if(bossCount[1] > 0 && bossCount[0] <= 0) 
        {
            enemiesCount += 401;
        }
        else 
        {
            enemiesCount = FindObjectsOfType<Enemy>().Length;
        }

        if(enemiesCount <= 0)
        {
            musicSfxObject.SetActive(true);
            battleSfxObject.SetActive(false);
            battleBossSfxObject.SetActive(false);
        }
        else if(enemiesCount > 0 && enemiesCount < 400)
        {
            musicSfxObject.SetActive(false);
            battleSfxObject.SetActive(true);
            battleBossSfxObject.SetActive(false);
        } 
        else if(enemiesCount >= 401)
        {
            musicSfxObject.SetActive(false);
            battleSfxObject.SetActive(false);
            battleBossSfxObject.SetActive(true);
        }   
    }

    private void SetCursor()
    {
        if(Time.timeScale == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        if(statPlayer.currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameOverPanel.SetActive(true);
        }
    }

#region SettingFunction
    public void SetMusicSound()
    {
        settingData.musicSound = musicSlider.value;
        musicSfx.volume = settingData.musicSound;
        battleSfx.volume = settingData.musicSound;
        battleBossSfx.volume = settingData.musicSound;
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

    public void mainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        saveManager.SaveGame();
        inven.slotWeapons[0] = inven.WeaponInventory[2];
        inven.slotWeapons[1] = null;
        statPlayer.hasGhost = false;
        calculateData.serverDestroy = 0;
        calculateData.enemiesKill = 0;
        calculateData.goldCount = 0;
        calculateData.rescueWolf = 0;
    }

    public void Lobby()
    {
        saveManager.SaveGame();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        statPlayer.hasGhost = false;
        inven.slotWeapons[0] = inven.WeaponInventory[2];
        inven.slotWeapons[1] = null;
        SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Single);
    }

    public void HowToPlay()
    {
        howtoplayPanel.SetActive(true);
    }

    public void SetFinalScore()
    {
        calculateData.goldCoinsPocket += calculateData.total;
        inven.slotWeapons[0] = inven.WeaponInventory[2];
        inven.slotWeapons[1] = null;
        statPlayer.hasGhost = false;
        calculateData.serverDestroy = 0;
        calculateData.enemiesKill = 0;
        calculateData.goldCount = 0;
        calculateData.rescueWolf = 0;
        saveManager.SaveGame();
    }

    public void nextToCal()
    {   
        pausePanel.SetActive(false);
        pauseInGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        calculatePanel.SetActive(true);
        saveManager.SaveGame();
    }

    public void SettingPanel()
    {
        pausePanel.SetActive(false);
        pauseInGamePanel.SetActive(false);
        howtoplayPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void FinishSetting()
    {
        musicSlider.value = settingData.musicSound;
        graphicValue.value = settingData.qualityIndex;
        fullScreenValue.isOn = settingData.isFullScreen;

        settingPanel.SetActive(false);

        if(lobby)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pauseInGamePanel.SetActive(true);
        } 

        saveManager.SaveGame();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseInGamePanel.SetActive(false);
        settingPanel.SetActive(false);
        saveManager.SaveGame();
    }

    public void Quit()
    {
        Application.Quit();
        saveManager.SaveGame();
    }

    
#endregion

    void OnEnable()
    {
        escape.Enable();
    }

    void OnDisable()
    {
        escape.Disable();
    }
}
