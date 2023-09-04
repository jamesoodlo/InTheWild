using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyUpgradeSystem : MonoBehaviour
{
    void Start()
    {
        int numDontDestroyUpgradeSystem = FindObjectsOfType<DontDestroyUpgradeSystem>().Length;
        if (numDontDestroyUpgradeSystem != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
