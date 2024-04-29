using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    //points system to keep track of points between scenes
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
    
   
}
