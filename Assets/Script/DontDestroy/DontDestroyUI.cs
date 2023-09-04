using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyUI : MonoBehaviour
{

    void Start()
    {
        int numDontDestroyUI = FindObjectsOfType<DontDestroyUI>().Length;
        if (numDontDestroyUI != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
