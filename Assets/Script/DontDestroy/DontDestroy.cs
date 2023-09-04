using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    void Awake()
    {
        
        
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject.transform);
    }
}
