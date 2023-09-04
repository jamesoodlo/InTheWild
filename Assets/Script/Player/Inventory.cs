using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    Stats statPlayer;
    Animator _animator;
    public UIManger uiManager;
    public CalculateScoreData calculateData;

    [Header("Weapons Slot")]
    public int weaponSlots;
    public weapon[] slotWeapons;
    public weapon currentWeapon;
    
    [Header("Weapon Inventory")]
    public weapon[] WeaponInventory;

    [Header("Weapon In Bag")]
    public GameObject[] WeaponInBag;

    [Header("Weapons Objects")]
    public GameObject[] WeaponsObjects;

    [Header("Weapons Inventory Check")]
    public bool hasAxe;
    public bool hasBlade;
    public bool hasSpear;
    public bool hasHammer;
    public bool hasLaser;

    [Header("Coins In Pocket")]
    public int goldCoinsPocket;

    [Header("Weapons Attack Check")]
    public int atkClick;

    [Header("Items/Weapon Range Check")]
    public bool itemRange;
    public bool boxRange;
    public bool getItem;
    public bool campfireRange;
    private GameObject findItem;
    private WeaponItems foundItem;
    private GameObject findItemOnly;
    private ItemsOnly foundItemOnly;
    private GameObject findCampfire;
    private Campfire campfire;

#region Find Weapons Component
    [Header("Find Weapons Component")]
    private GameObject findWeapons;
    private WeaponItems foundAxe;
    private WeaponItems foundBlade;
    private WeaponItems foundSpear;
    private WeaponItems foundHammer;
    private WeaponItems foundLaser;
#endregion

    public void Start() 
    {
        statPlayer = GetComponent<Stats>();
        _animator = GetComponent<Animator>();
        slotWeapons[1] = null;
        if(weaponSlots == 0)
        {
            currentWeapon = slotWeapons[0];
        }
        else
        {
            currentWeapon = slotWeapons[1];
        }
    }

    private void Update() 
    {
        SetSlot();
        SetWeaponInBag();
        goldCoinsPocket = calculateData.goldCoinsPocket;

        if(getItem)
        {
            changeWeapon(); 
        }

        if(statPlayer.currentHealth <= 0)
        {
            slotWeapons[1] = null;
        }

        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("PickUp"))
        {
            itemRange = false;
        }
    }


    public void EnabledDmg()
    {
        if(weaponSlots == 0)
        {
            slotWeapons[0].EnabledDamageCollider();
        }
        else
        {  
            if( slotWeapons[1] != null)
            {
                slotWeapons[1].EnabledDamageCollider();
            } 
        }  
    }

    public void DisabledDmg()
    {
        if(weaponSlots == 0)
        {
            slotWeapons[0].DisabledDamageCollider();
        }
        else
        {
            if( slotWeapons[1] != null)
            {
                slotWeapons[1].DisabledDamageCollider();
            }  
        }      
    }


    private void SetSlot()
    {
        if(weaponSlots == 0)
        {
            if(slotWeapons[0].isAxe)
            {
                WeaponsObjects[0].SetActive(true);
                hasAxe = true;  

                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[0].isBlade)
            {
                WeaponsObjects[1].SetActive(true);
                hasBlade = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[0].isSpear)
            {
                WeaponsObjects[2].SetActive(true);
                hasSpear = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[0].isHammer)
            {
                WeaponsObjects[3].SetActive(true);
                hasHammer = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[0].isLaser)
            {
                WeaponsObjects[4].SetActive(true);
                hasLaser = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
            }
        }
        else
        {
            if(slotWeapons[1].isAxe)
            {
                WeaponsObjects[0].SetActive(true);
                hasAxe = true;  

                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[1].isBlade)
            {
                WeaponsObjects[1].SetActive(true);
                hasBlade = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[1].isSpear)
            {
                WeaponsObjects[2].SetActive(true);
                hasSpear = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[1].isHammer)
            {
                WeaponsObjects[3].SetActive(true);
                hasHammer = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[4].SetActive(false);
                hasLaser = false;
            }
            else if(slotWeapons[1].isLaser)
            {
                WeaponsObjects[4].SetActive(true);
                hasLaser = true;  

                WeaponsObjects[0].SetActive(false);
                hasAxe = false;
                WeaponsObjects[1].SetActive(false);
                hasBlade = false;
                WeaponsObjects[2].SetActive(false);
                hasSpear = false;
                WeaponsObjects[3].SetActive(false);
                hasHammer = false;
            }
        }     
    }

    private void SetWeaponInBag()
    {
        if(weaponSlots == 0)
        {
            if(slotWeapons[1] != null)
            {
                if(slotWeapons[1].isAxe)
                {
                    WeaponInBag[0].SetActive(true); 
                    WeaponInBag[1].SetActive(false);
                    WeaponInBag[2].SetActive(false);
                    WeaponInBag[3].SetActive(false);
                    WeaponInBag[4].SetActive(false);
                }
                else if(slotWeapons[1].isBlade)
                {
                    WeaponInBag[0].SetActive(false); 
                    WeaponInBag[1].SetActive(true);
                    WeaponInBag[2].SetActive(false);
                    WeaponInBag[3].SetActive(false);
                    WeaponInBag[4].SetActive(false);
                }
                else if(slotWeapons[1].isSpear)
                {
                    WeaponInBag[0].SetActive(false); 
                    WeaponInBag[1].SetActive(false);
                    WeaponInBag[2].SetActive(true);
                    WeaponInBag[3].SetActive(false);
                    WeaponInBag[4].SetActive(false);
                }
                else if(slotWeapons[1].isHammer)
                {
                    WeaponInBag[0].SetActive(false); 
                    WeaponInBag[1].SetActive(false);
                    WeaponInBag[2].SetActive(false);
                    WeaponInBag[3].SetActive(true);
                    WeaponInBag[4].SetActive(false);
                }
                else if(slotWeapons[1].isLaser)
                {
                    WeaponInBag[0].SetActive(false); 
                    WeaponInBag[1].SetActive(false);
                    WeaponInBag[2].SetActive(false);
                    WeaponInBag[3].SetActive(false);
                    WeaponInBag[4].SetActive(true);
                }
            }
            else
            {
                WeaponInBag[0].SetActive(false); 
                WeaponInBag[1].SetActive(false);
                WeaponInBag[2].SetActive(false);
                WeaponInBag[3].SetActive(false);
                WeaponInBag[4].SetActive(false);
            }
        }
        else
        {
            if(slotWeapons[0].isAxe)
            {
                WeaponInBag[0].SetActive(true); 
                WeaponInBag[1].SetActive(false);
                WeaponInBag[2].SetActive(false);
                WeaponInBag[3].SetActive(false);
                WeaponInBag[4].SetActive(false);
            }
            else if(slotWeapons[0].isBlade)
            {
                WeaponInBag[0].SetActive(false); 
                WeaponInBag[1].SetActive(true);
                WeaponInBag[2].SetActive(false);
                WeaponInBag[3].SetActive(false);
                WeaponInBag[4].SetActive(false);
            }
            else if(slotWeapons[0].isSpear)
            {
                WeaponInBag[0].SetActive(false); 
                WeaponInBag[1].SetActive(false);
                WeaponInBag[2].SetActive(true);
                WeaponInBag[3].SetActive(false);
                WeaponInBag[4].SetActive(false);
            }
            else if(slotWeapons[0].isHammer)
            {
                WeaponInBag[0].SetActive(false); 
                WeaponInBag[1].SetActive(false);
                WeaponInBag[2].SetActive(false);
                WeaponInBag[3].SetActive(true);
                WeaponInBag[4].SetActive(false);
            }
            else if(slotWeapons[0].isLaser)
            {
                WeaponInBag[0].SetActive(false); 
                WeaponInBag[1].SetActive(false);
                WeaponInBag[2].SetActive(false);
                WeaponInBag[3].SetActive(false);
                WeaponInBag[4].SetActive(true);
            }
        }
        
    }

    private void changeWeapon()
    {
        if(slotWeapons[1] == null)
        {
            if(foundAxe != null)
            {
                slotWeapons[1] = WeaponInventory[0];
                if(slotWeapons[1] == WeaponInventory[0])
                {
                    findWeapons = null;
                    foundAxe = null;
                }
            }
            
            if(foundBlade != null)
            {
                slotWeapons[1] = WeaponInventory[1];
                if(slotWeapons[1] == WeaponInventory[1])
                {
                    findWeapons = null;
                    foundBlade = null;
                }
            }
            
            if(foundSpear != null)
            {
                slotWeapons[1] = WeaponInventory[2];
                if(slotWeapons[1] = WeaponInventory[2])
                {
                    findWeapons = null;
                    foundSpear = null;
                }
            }
           
            if(foundHammer != null)
            {
                slotWeapons[1] = WeaponInventory[3];
                if(slotWeapons[1] == WeaponInventory[3])
                {
                    findWeapons = null;
                    foundHammer = null;
                }
                
            }
            
            if(foundLaser != null)
            {
                slotWeapons[1] = WeaponInventory[4];
                if(slotWeapons[1] == WeaponInventory[4])
                {
                    findWeapons = null;
                    foundLaser = null;
                }
            }
        }
        else
        {
            if(weaponSlots == 0)
            {
                if(foundAxe != null)
                {
                    slotWeapons[0] = WeaponInventory[0];
                    if(slotWeapons[0] == WeaponInventory[0])
                    {
                        findWeapons = null;
                        foundAxe = null;
                    }
                }
                
                if(foundBlade != null)
                {
                    slotWeapons[0] = WeaponInventory[1];
                    if(slotWeapons[0] == WeaponInventory[1])
                    {
                        findWeapons = null;
                        foundBlade = null;
                    }
                }
                
                if(foundSpear != null)
                {
                    slotWeapons[0] = WeaponInventory[2];
                    if(slotWeapons[0] = WeaponInventory[2])
                    {
                        findWeapons = null;
                        foundSpear = null;
                    }
                }
            
                if(foundHammer != null)
                {
                    slotWeapons[0] = WeaponInventory[3];
                    if(slotWeapons[0] == WeaponInventory[3])
                    {
                        findWeapons = null;
                        foundHammer = null;
                    }
                    
                }
                
                if(foundLaser != null)
                {
                    slotWeapons[0] = WeaponInventory[4];
                    if(slotWeapons[0] == WeaponInventory[4])
                    {
                        findWeapons = null;
                        foundLaser = null;
                    }
                }
            }
            else
            {
                if(foundAxe != null)
                {
                    slotWeapons[1] = WeaponInventory[0];
                    if(slotWeapons[1] == WeaponInventory[0])
                    {
                        findWeapons = null;
                        foundAxe = null;
                    }
                }
                
                if(foundBlade != null)
                {
                    slotWeapons[1] = WeaponInventory[1];
                    if(slotWeapons[1] == WeaponInventory[1])
                    {
                        findWeapons = null;
                        foundBlade = null;
                    }
                }
                
                if(foundSpear != null)
                {
                    slotWeapons[1] = WeaponInventory[2];
                    if(slotWeapons[1] = WeaponInventory[2])
                    {
                        findWeapons = null;
                        foundSpear = null;
                    }
                }
            
                if(foundHammer != null)
                {
                    slotWeapons[1] = WeaponInventory[3];
                    if(slotWeapons[1] == WeaponInventory[3])
                    {
                        findWeapons = null;
                        foundHammer = null;
                    }
                    
                }
                
                if(foundLaser != null)
                {
                    slotWeapons[1] = WeaponInventory[4];
                    if(slotWeapons[1] == WeaponInventory[4])
                    {
                        findWeapons = null;
                        foundLaser = null;
                    }
                }
            }    
        }
         
    }

     private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == "Menu")
        {
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Items")
        {
            itemRange = true;

            findItem = GameObject.FindGameObjectWithTag("Items");       
            foundItem = findItem.GetComponent<WeaponItems>();
        }

        if (collision.gameObject.tag == "ItemsBox")
        {
            boxRange = true;
        }

        if (collision.gameObject.tag == "Campfire")
        {
            findCampfire = GameObject.Find("Campfire");
            campfire = findCampfire.GetComponent<Campfire>();
            campfireRange = true;
        }

        if (collision.gameObject.tag == "Axe")
        {
            itemRange = true;
        
            findWeapons = GameObject.FindGameObjectWithTag("Axe");       
            foundAxe = findWeapons.GetComponent<WeaponItems>(); 
        }
        else if (collision.gameObject.tag == "Blade")
        {
            itemRange = true;

            findWeapons = GameObject.FindGameObjectWithTag("Blade");       
            foundBlade = findWeapons.GetComponent<WeaponItems>(); 
        }
        else if (collision.gameObject.tag == "Spear")
        {
            itemRange = true;

            findWeapons = GameObject.FindGameObjectWithTag("Spear");       
            foundSpear = findWeapons.GetComponent<WeaponItems>(); 
        }
        else if (collision.gameObject.tag == "Hammer")
        {
            itemRange = true;

            findWeapons = GameObject.FindGameObjectWithTag("Hammer");       
            foundHammer = findWeapons.GetComponent<WeaponItems>(); 
        }
        else if (collision.gameObject.tag == "Laser")
        {
            itemRange = true;

            findWeapons = GameObject.FindGameObjectWithTag("Laser");       
            foundLaser = findWeapons.GetComponent<WeaponItems>(); 
        }

        else if (collision.gameObject.tag == "Apple")
        {
            itemRange = true;

            findItemOnly = GameObject.FindGameObjectWithTag("Apple");
            foundItemOnly = findItemOnly.GetComponent<ItemsOnly>();
        }

        else if (collision.gameObject.tag == "GoldCoin")
        {
            itemRange = true;

            findItemOnly = GameObject.FindGameObjectWithTag("GoldCoin");
            foundItemOnly = findItemOnly.GetComponent<ItemsOnly>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Items")
        {
            itemRange = false;    
            findItem = null;  
            foundItem = null;  
        }

        if (collision.gameObject.tag == "ItemsBox")
        {
            boxRange = false;
        }

        if (collision.gameObject.tag == "Campfire")
        {
            findCampfire = null;
            campfire = null;
            campfireRange = false;
        }

        switch (collision.gameObject.tag)
        {
            case "Axe":
                itemRange = false;
                findWeapons = null;
                foundAxe = null;
                break;
            case "Blade":
                itemRange = false;
                findWeapons = null;
                foundBlade = null;
                break;
            case "Spear":
                itemRange = false;
                findWeapons = null;
                foundSpear = null;
                break;
            case "Hammer":
                itemRange = false;
                findWeapons = null;
                foundHammer = null;
                break;
            case "Laser":
                itemRange = false;
                findWeapons = null;
                foundLaser = null;
                break;
            case "Apple":
                itemRange = false;
                findItemOnly = null;
                foundItemOnly = null;
                break;
            case "GoldCoin":
                itemRange = false;
                findItemOnly = null;
                foundItemOnly = null;
                break;
        }

    }

    
}
