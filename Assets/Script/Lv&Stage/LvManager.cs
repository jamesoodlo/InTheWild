using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvManager : MonoBehaviour
{
    private bool get;
    
    [Header("Level Check")]
    public bool Menu;
    public bool lobby;
    public bool lv1;
    public bool lv2;
    public bool lv3;
    public bool lv4;
    public bool lv5;
    public bool lv6;

    [Header("Level Machanics")]
    public int stageCount;
    public StageFighting[] stage;
    public GameObject wallLvBlocking;
    public GameObject victoryPanel;

    private GameObject findMiniBoss;
    public EnemyMK2Stats miniBoss;

    private GameObject findBoss;
    public EnemyMK3Stats Boss;

    private GameObject findServer;
    public Server server;

    void Start()
    {
        tagChanging();

        if(lv4)
        {
            findMiniBoss = GameObject.Find("Mk2");
            miniBoss = findMiniBoss.GetComponent<EnemyMK2Stats>();
            
        }
        else if(lv5)
        {
            findBoss = GameObject.Find("Mk3");
            Boss = findBoss.GetComponent<EnemyMK3Stats>();
        }
        else if(lv6)
        {
            findServer = GameObject.Find("Server");
            server = findServer.GetComponent<Server>();  
        }
    }

 
    void Update()
    {
        LvBlocking();
    }

    private void LvBlocking()
    {
        if(lobby)
        {
            
        }
        else if(lv1 || lv2 || lv3)
        {
            stage = FindObjectsOfType<StageFighting>();

            if(stage[0].stageClear && stage[1].stageClear && stage[2].stageClear && stage[3].stageClear)
            {
                wallLvBlocking.SetActive(false);
            }
        }
        else if(lv4)
        {
            stage = FindObjectsOfType<StageFighting>();

            if(stage[0].stageClear && stage[1].stageClear && stage[2].stageClear && stage[3].stageClear && miniBoss.currenthealth <= 0)
            {
                wallLvBlocking.SetActive(false);
            }
        }
        else if(lv5)
        {
            stage = FindObjectsOfType<StageFighting>();

            if(stage[0].stageClear && stage[1].stageClear && stage[2].stageClear && stage[3].stageClear && stage[4].stageClear && Boss.currenthealth <= 0)
            {
                wallLvBlocking.SetActive(false);
            }
        }
        else if(lv6)
        {   
            if(server.currenthealth <= 0)
            {
                
                if(!get)
                {
                    victoryPanel.SetActive(true);
                    get = true;
                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }


    private void tagChanging()
    {
        if(lobby)
        {
            this.tag = "Lobby";
        }
        else if(lv1)
        {
            this.tag = "Lv1";
        }
        else if(lv2)
        {
            this.tag = "Lv2";
        }
        else if(lv3)
        {
            this.tag = "Lv3";
        }
        else if(lv4)
        {
            this.tag = "Lv4";
        }
        else if(lv5)
        {
            this.tag = "Lv5";
        }
        else if(lv6)
        {
            this.tag = "Lv6";
        }
        else if(Menu)
        {
            this.tag = "Menu";
        }
    }

}
