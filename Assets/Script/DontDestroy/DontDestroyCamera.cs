using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCamera : MonoBehaviour
{
    
    void Start()
    {
        int numDontDestroyCamera = FindObjectsOfType<DontDestroyCamera>().Length;
        if (numDontDestroyCamera != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
