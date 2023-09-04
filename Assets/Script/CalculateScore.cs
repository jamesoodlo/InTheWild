using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CalculateScore : MonoBehaviour
{
    public CalculateScoreData calculateData;

    public TextMeshProUGUI enemiesKill;
    public TextMeshProUGUI rescueWolf;
    public TextMeshProUGUI serverDestroy;
    public TextMeshProUGUI goldCount;
    public TextMeshProUGUI total;

    private void Update() 
    {
        calculateData.total = calculateData.enemiesKill + calculateData.rescueWolf + calculateData.goldCount + calculateData.serverDestroy;

        enemiesKill.text  = calculateData.enemiesKill.ToString();
        rescueWolf.text  = calculateData.rescueWolf.ToString();
        serverDestroy.text = calculateData.serverDestroy.ToString();
        goldCount.text  = calculateData.goldCount.ToString();
        total.text  = calculateData.total.ToString();
    }

}
