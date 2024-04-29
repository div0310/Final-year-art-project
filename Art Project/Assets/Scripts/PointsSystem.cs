using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    string pointsKey = "Points";
    public int CurrentPoints
    {
        get; set;
    }

    private void Awake()
    {
        CurrentPoints = PlayerPrefs.GetInt(pointsKey);
    }

    public void SetPoints(int points)
    {
        PlayerPrefs.SetInt(pointsKey, points);
    }
    //UPDATE METHOD CHECK IF CURRENT POINTS >= 10 
    //PLAY VICTORY SCENE
   
}
