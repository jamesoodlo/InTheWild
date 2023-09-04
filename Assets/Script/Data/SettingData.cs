using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingData", menuName = "", order = 0)]
public class SettingData : ScriptableObject 
{
    [Header("All Setting")]
    public float musicSound;
    public int qualityIndex;
    public bool isFullScreen;

    [Header("Menu Setting")]
    public bool isFirstPlay;
}
