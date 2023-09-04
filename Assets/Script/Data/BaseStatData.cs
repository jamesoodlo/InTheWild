using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BaseStatData", menuName = "", order = 0)]
public class BaseStatData : ScriptableObject 
{
    public bool onCharacter;

    [Header("Base Stats")]
    public float baseHP;
    public float baseSTM;
    public float baseSTMRe;
    public int baseATK;
    public float baseSPD;

    [Header("Upgrade Cost")]
    public int upgradeHPCost;
    public int upgradeSTMCost;
    public int upgradeSTMReCost;
    public int upgradeATKCost;
    public int upgradeSPDCost;

    [Header("Starndard Stats")]
    public float stdHP;
    public float stdSTM;
    public float stdSTMRe;
    public int stdATK;
    public float stdSPD;

}
