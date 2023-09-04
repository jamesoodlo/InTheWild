using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    Inventory inven;

    [Header("Slot1")]
    public Slider cdBar1;
    public GameObject cdBar1Prefab;
    public bool canSkill1;
    public float maxCdSkill1;
    public float currentCdSkill1;

    [Header("Slot2")]
    public Slider cdBar2;
    public GameObject cdBar2Prefab;
    public bool canSkill2;
    public float maxCdSkill2;
    public float currentCdSkill2;

    void Start()
    {
        inven = GetComponentInParent<Inventory>();
        SetCDSkill();
        canSkill1 = true;
        currentCdSkill1 = maxCdSkill1;
        currentCdSkill2 = maxCdSkill2;
    }

    void Update()
    {
        SetCdBar();
        SetCDSkill();
        CoolDownSkill1();
        CoolDownSkill2();
    }

    public void SetCdBar()
    {
        cdBar1.maxValue = maxCdSkill1;
        cdBar1.value = currentCdSkill1;

        cdBar2.maxValue = maxCdSkill2;
        cdBar2.value = currentCdSkill2;

        if(inven.weaponSlots == 0)
        {
            cdBar1Prefab.SetActive(true);
            cdBar2Prefab.SetActive(false);
        }
        else
        {
            cdBar1Prefab.SetActive(false);
            cdBar2Prefab.SetActive(true);
        }

       
    }

    public void SetCDSkill()
    {
        maxCdSkill1 = inven.slotWeapons[0].CdSkill;

        if(inven.slotWeapons[1] != null)
        {
            maxCdSkill2 = inven.slotWeapons[1].CdSkill;
        }
    }

    public void CoolDownSkill1()
    {
        if(currentCdSkill1 > maxCdSkill1)
        {
            currentCdSkill1 = maxCdSkill1;
        }

        if(currentCdSkill1 == maxCdSkill1)
        {
            canSkill1 = true;
        }

        if(!canSkill1)
        {
            currentCdSkill1 += 1 * Time.deltaTime;
        }

        if(currentCdSkill1 != maxCdSkill1)
        {
            canSkill1 = false;
        }
        else
        {
            
        }
    }

    public void CoolDownSkill2()
    {
        if(currentCdSkill2 > maxCdSkill2)
        {
            currentCdSkill2 = maxCdSkill2;
        }

        if(currentCdSkill2 == maxCdSkill2)
        {
            canSkill2 = true;
        }

        if(!canSkill2)
        {
            currentCdSkill2 += 1 * Time.deltaTime;
        }

        if(currentCdSkill2 != maxCdSkill2)
        {
            canSkill2 = false;
        }
        else
        {
            
        }
    }
}
