using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CalculateScoreData", menuName = "", order = 0)]
public class CalculateScoreData : ScriptableObject 
{
    public int enemiesKill;
    public int goldCount;
    public int rescueWolf;
    public int serverDestroy;
    public int total;

    public int goldCoinsPocket;

    public int mk1Score = 1;
    public int mk2Score = 10;
    public int mk3Score = 20;
    public int serverScore = 40;

    private void Update() 
    {
        
    }
}
