using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    private GameObject findPlayer;
    private Inventory inven;
    private Stats statPlayer;
    private GameObject findGhost;
    private GhostAI ghost;

    private int ghostCount;
    public int randomDrop;
    public int[] randomNum = {0, 1, 2, 3};
    public Transform spawnPoint;
    public Transform spawnPointWplayer;
    public GameObject ghostDummy;
    public GameObject ghostPrefab;

    public GameObject cageDoor;
    public ParticleSystem hitFx;
    public AudioSource hitSound;
    public AudioSource doorDestroySound;

    public bool doorOpen;

    public int maxHealth = 200;
    public int currenthealth;

    void Start()
    {
        currenthealth = maxHealth;
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        statPlayer = findPlayer.GetComponent<Stats>(); 
        hitFx.Stop();
        if(statPlayer.hasGhost)
        {
            randomDrop = 0;
            while(ghostCount < 1)
            {
                Instantiate(ghostPrefab, spawnPointWplayer.transform.position, spawnPointWplayer.transform.rotation);
                ghostCount += 1;  
            }   
        }
        else
        {
            randomDrop = randomNum[Random.Range(0, randomNum.Length)];
        }
    }

    void Update()
    {
        if(statPlayer.hasGhost)
        {
            hitFx.Stop();
            cageDoor.SetActive(false);
            ghostDummy.SetActive(false);
        }
        else
        {
            if(randomDrop == 1 || randomDrop == 2 || randomDrop == 3)
            {
                hitFx.Stop();
                cageDoor.SetActive(false);
                ghostDummy.SetActive(false);
            }
            else 
            {
                if(currenthealth <= 0)
                {
                    ghostDummy.SetActive(false);
                    hitFx.Stop();
                    while(ghostCount < 1)
                    {
                        Instantiate(ghostPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                        ghostCount += 1;  
                    }   
                    
                    Destroy(cageDoor.gameObject);
                }
            }
        }
        
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapons")
        { 
            if(!statPlayer.hasGhost)
            {
                hitFx.Play();
                hitSound.Play();
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
                
            }
            
        }
        else
        {
            hitFx.Stop();
        }
    }
}
