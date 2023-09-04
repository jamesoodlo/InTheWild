using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class itemBox : MonoBehaviour
{

    [Header("Items Prefab")]
    public GameObject canvas;
    public GameObject itemsAndWeapon;
    private WeaponItems itemsWeapons;

    [Header("Animator")]
    Animator anim;

    [Header("Etc.")]
    public Transform spawnPoint;
    public bool inRange;
    public bool canGet;

    [Header("Find Inventory.")]
    private GameObject findInven;
    private Inventory inven;

    public Collider collisionBox;
    public Collider Lid;
    public Collider Box;

    void Start()
    {
        anim = GetComponent<Animator>();
        canvas.SetActive(false);
        StartCoroutine(SetItemFalse());
    }

    void Update()
    {
        findInven = GameObject.Find("Himba");
        inven = findInven.GetComponent<Inventory>();
        itemsWeapons = itemsAndWeapon.GetComponent<WeaponItems>();

        if(inRange == true && inven.getItem == true)
        {
            anim.SetBool("isOpen", true);
            canvas.SetActive(false); 
            inven.boxRange = false;
            collisionBox.enabled = false;
            Lid.enabled = false;
            Box.enabled = false;
            StartCoroutine(SetItemTrue());
            if(itemsAndWeapon == true)
            {
                inRange = false;
                canvas.SetActive(false); 
            }
        }

        if(itemsAndWeapon == null)
        {
            canvas.SetActive(false); 
            Debug.Log("item missing");
        }

    }

    private IEnumerator SetItemTrue()
    {
        yield return new WaitForSeconds(1f);
        itemsAndWeapon.SetActive(true);
        //Instantiate(itemsAndWeapon, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    private IEnumerator SetItemFalse()
    {
        yield return new WaitForSeconds(2f);
        itemsAndWeapon.SetActive(false);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false; 
            canvas.SetActive(false);       
        }
    }
}
