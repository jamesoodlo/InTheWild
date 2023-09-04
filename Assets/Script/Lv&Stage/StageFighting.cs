using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFighting : MonoBehaviour
{
    public int enemiesCount;
    public bool hasBoss;
    public bool stageClear;
    public GameObject boss;
    public Enemy[] enemy;
    public GameObject enemiesPack;
    public GameObject[] wallBlocking;
    
    void Start()
    {
        enemiesPack.SetActive(false);
        allWallActivate();
        if(hasBoss) boss.SetActive(false);
        stageClear = false;
    }

    
    void Update()
    {
        enemy = FindObjectsOfType<Enemy>();
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        
        if(enemiesPack.activeSelf)
        {
            if(enemiesCount <= 0)
            {
                for (int i = 0; i < wallBlocking.Length; i++)
                {
                    if(enemiesCount <= 0)
                    {
                        //wallBlocking[i].SetActive(false);
                        Destroy(wallBlocking[i].gameObject);
                    }
                }
                stageClear = true;
            }
        }
    }

    private void allWallActivate()
    {

        for (int i = 0; i < wallBlocking.Length; i++)
        {
            if(wallBlocking[i] != null)
            {
                wallBlocking[i].SetActive(false);
            }

            if(enemiesCount <= 0)
            {
                wallBlocking[i].SetActive(false);
                Debug.Log("Clear");
            }            
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(hasBoss) boss.SetActive(true);
            enemiesPack.SetActive(true);

            for (int i = 0; i < wallBlocking.Length; i++)
            {
                wallBlocking[i].SetActive(true);
            }
        }
    }
}
