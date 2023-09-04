using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsOnly : MonoBehaviour
{
    public CalculateScoreData calculateData;
    
    [Header("Items Name")]
    public string itemsName;
    public string[] randomItems = {"Apple","GoldCoin"};

    [Header("Gold Coin Cost")]  
    public int goldCoinsCost;
    public int[] randomCoins = {1, 2, 3, 4, 5, 10, 15, 20};

    [Header("Items Check")]
    public bool isApple;
    public bool isCoin;

    [Header("Items Prefabs")]
    public GameObject apple;
    public GameObject coin;

    [Header("Sound Effect")]
    public AudioSource coinSfx;
    public AudioSource healSfx;

    [Header("Etc.")]
    public bool inRange;
    public bool canGet;
    private GameObject findPlayer;
    private Inventory inven;
    private Stats stat;

    void Start()
    {
        itemsName = randomItems[Random.Range(0, randomItems.Length)];
        goldCoinsCost = randomCoins[Random.Range(0, randomCoins.Length)];
        apple.SetActive(false);
        coin.SetActive(false);
    }

    void Update()
    {
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        stat = findPlayer.GetComponent<Stats>();
        
        GetItems();
        SetItems();
    }

    private void SetItems()
    {
        if(itemsName == "Apple")
        {
            isApple = true;
        }
        else if(itemsName == "GoldCoin")
        {
            isCoin = true;
        }

        if(isApple)
        {
            this.tag = "Apple";
            apple.SetActive(true);
            coin.SetActive(false);
        }
        else if(isCoin)
        {
            this.tag = "GoldCoin";
            apple.SetActive(false);
            coin.SetActive(true);
        }
    }


    private void GetItems()
    {
        if(inRange == true && inven.getItem == true)
        {
            inRange = false;
            if(isApple)
            {
                healSfx.Play();
                if(stat.currentHealth < stat.maxHealth)
                {
                    stat.currentHealth += 20;
                    if(stat.currentHealth > stat.maxHealth)
                    {
                        stat.currentHealth = stat.maxHealth;
                    }
                }
                Debug.Log("Healing!!");
                Destroy(apple);
                StartCoroutine(WaitAndDestroy());
            }
            else if(isCoin)
            {
                calculateData.goldCount += goldCoinsCost;
                coinSfx.Play();
                Debug.Log("Gold coin");
                Destroy(coin);
                StartCoroutine(WaitAndDestroy());
            }
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.55f);
        Destroy(this.gameObject);
    }

    private void setItem()
    {
        if(itemsName == "Apple")
        {
            isApple = true;
        }
        else if(itemsName == "GoldCoin")
        {
            isCoin = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

     void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;        
        }
    }
}
