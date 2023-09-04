using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponItems : MonoBehaviour
{
    [Header("Weapons/Items Name")]
    public string itemsName;
    public string[] randomItems;

    [Header("Sound Effect")]
    public AudioSource coinSfx;
    public AudioSource healSfx;

    [Header("Gold Coin Cost")]  
    public int goldCoinsCost;
    public int[] randomCoins = {1, 2, 3, 4, 5, 10, 15, 20};

    [Header("Weapons/Items Check")]
    public bool isAxe;
    public bool isBlade;
    public bool isSpear;
    public bool isHammer;
    public bool isLaser;
    public bool isApple;
    public bool isCoin;

    [Header("Weapons/Items Prefabs")]
    public GameObject axe;
    public GameObject blade;
    public GameObject spear;
    public GameObject hammer;
    public GameObject laser;
    public GameObject apple;
    public GameObject coin;

    [Header("Etc.")]
    public CalculateScoreData calculateData;
    public bool inRange;
    public bool canGet;
    private GameObject findPlayer;
    private Inventory inven;
    private Stats stat;

    private void Start() 
    {
        itemsName = randomItems[Random.Range(0, randomItems.Length)];
        goldCoinsCost = randomCoins[Random.Range(0, randomCoins.Length)];
        setItem();
        axe.SetActive(false);
        blade.SetActive(false);
        spear.SetActive(false);
        hammer.SetActive(false);
        laser.SetActive(false);
        apple.SetActive(false);
        coin.SetActive(false);
    }

    private void Update() 
    {
        findPlayer = GameObject.Find("Himba");
        inven = findPlayer.GetComponent<Inventory>();
        stat = findPlayer.GetComponent<Stats>();

        //setItem();
        if(isAxe)
        {
            this.tag = "Axe";
            axe.SetActive(true);
            blade.SetActive(false);
            spear.SetActive(false);
            hammer.SetActive(false);
            laser.SetActive(false);
            apple.SetActive(false);
        }
        else if(isBlade)
        {
            this.tag = "Blade";
            axe.SetActive(false);
            blade.SetActive(true);
            spear.SetActive(false);
            hammer.SetActive(false);
            laser.SetActive(false);
            apple.SetActive(false);
        }
        else if(isSpear)
        {
            this.tag = "Spear";
            axe.SetActive(false);
            blade.SetActive(false);
            spear.SetActive(true);
            hammer.SetActive(false);
            laser.SetActive(false);
            apple.SetActive(false);
        }
        else if(isHammer)
        {
            this.tag = "Hammer";
            axe.SetActive(false);
            blade.SetActive(false);
            spear.SetActive(false);
            hammer.SetActive(true);
            laser.SetActive(false);
            apple.SetActive(false);
        }
        else if(isLaser)
        {
            this.tag = "Laser";
            axe.SetActive(false);
            blade.SetActive(false);
            spear.SetActive(false);
            hammer.SetActive(false);
            laser.SetActive(true);
            apple.SetActive(false);
        }
        else if(isApple)
        {
            this.tag = "Apple";
            axe.SetActive(false);
            blade.SetActive(false);
            spear.SetActive(false);
            hammer.SetActive(false);
            laser.SetActive(false);
            apple.SetActive(true);
        }
        else if(isCoin)
        {
            this.tag = "GoldCoin";
            axe.SetActive(false);
            blade.SetActive(false);
            spear.SetActive(false);
            hammer.SetActive(false);
            laser.SetActive(false);
            apple.SetActive(false);
            coin.SetActive(true);
        }
        GetItemWeapon();     
    }

    private void GetItemWeapon()
    {
        if(inRange == true && inven.getItem == true)
        {
            inRange = false;
            if(isApple)
            {
                healSfx.Play();
                apple.SetActive(false);
                if(stat.currentHealth < stat.maxHealth)
                {
                    stat.currentHealth += 20;
                    if(stat.currentHealth > stat.maxHealth)
                    {
                        stat.currentHealth = stat.maxHealth;
                    }
                }
                StartCoroutine(WaitAndDestroy());
            }
            else if(isCoin)
            {
                coinSfx.Play();
                coin.SetActive(false);
                calculateData.goldCount += goldCoinsCost;
                StartCoroutine(WaitAndDestroy());
            }
            else if(inven.slotWeapons[1] == null)
            {
                inven.itemRange = false;
                //Destroy(this.gameObject);
                StartCoroutine(DestroyItem());
            }
            else if(inven.slotWeapons[0] != null || inven.slotWeapons[1] != null)
            {
                inven.itemRange = false;
                //Destroy(this.gameObject);
                StartCoroutine(DestroyItem());
            }
              
            
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.55f);
        Destroy(this.gameObject);
    }

    private IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    private void setItem()
    {
        if(itemsName == "BattleAxe")
        {
            isAxe = true;
        }
        else if(itemsName == "BattleBlade")
        {
            isBlade = true;
        }
        else if(itemsName == "BattleSpear")
        {
            isSpear = true;
        }
        else if(itemsName == "StoneHammer")
        {
            isHammer = true;
        }
        else if(itemsName == "LightSaber")
        {
            isLaser = true;
        }
        else if(itemsName == "HealingApple")
        {
            isApple = true;
        }
        else if(itemsName == "GoldCoin")
        {
            isCoin = true;
        }
    }


#region SetTrueWeaponBoolWithTime
    private IEnumerator SetTrueAxe()
    {
        yield return new WaitForSeconds(0.5f);
        isAxe = true;
        itemsName = "BattleAxe";
    }

    private IEnumerator SetTrueBlade()
    {
        yield return new WaitForSeconds(0.5f);
        isBlade = true;
        itemsName = "BattleBlade";
    }

    private IEnumerator SetTrueSpear()
    {
        yield return new WaitForSeconds(0.5f);
        isSpear = true;
        itemsName = "BattleSpear";
    }

    private IEnumerator SetTrueHammer()
    {
        yield return new WaitForSeconds(0.5f);
        isHammer = true;
        itemsName = "StoneHammer";
    }

    private IEnumerator SetTrueLaser()
    {
        yield return new WaitForSeconds(0.5f);
        isLaser = true;
        itemsName = "LightSaber";
    }
#endregion

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
