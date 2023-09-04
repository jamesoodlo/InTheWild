using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBox : MonoBehaviour
{
    [Header("Health System")]
    public int maxHealthBox = 80;
    public int currentHealthBox;

    [Header("Items Prefab")]
    public Transform spawnPoint;
    public GameObject woodBox;
    public GameObject items;
    private int itemCount;

    public AudioSource hitSound;

    [Header("Find Inventory.")]
    private GameObject findInven;
    private Inventory inven;

    void Start()
    {
        currentHealthBox = maxHealthBox;
        items.SetActive(false);
    }

    void Update()
    {
        findInven = GameObject.Find("Himba");
        inven = findInven.GetComponent<Inventory>();

        if(currentHealthBox <= 0)
        {
            woodBox.SetActive(false);
            items.SetActive(true);
            hitSound.Stop();
            StartCoroutine(SetNull());
            while(itemCount < 1)
            {
                Instantiate(items, spawnPoint.transform.position, spawnPoint.transform.rotation);
                itemCount += 1;  
            }   
        }
    }

    IEnumerator SetNull()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Weapons")
        { 
            hitSound.Play();
            currentHealthBox -= 30;
        }
    }
}
