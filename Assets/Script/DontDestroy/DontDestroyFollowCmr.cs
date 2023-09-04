using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyFollowCmr : MonoBehaviour
{
   
    void Start()
    {
        int numDontDestroyFollowCmr = FindObjectsOfType<DontDestroyFollowCmr>().Length;
        if (numDontDestroyFollowCmr != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
