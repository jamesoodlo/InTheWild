using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPlayer : MonoBehaviour
{
    void Start()
    {
        int numDontDestroyPlayer = FindObjectsOfType<DontDestroyPlayer>().Length;
        if (numDontDestroyPlayer != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
