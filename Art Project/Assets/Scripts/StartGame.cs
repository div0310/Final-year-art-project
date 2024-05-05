using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    PointsSystem pointSystem;//initialize pointsystem

    // Start is called before the first frame update
    void Start()
    {
        pointSystem = FindObjectOfType<PointsSystem>();
        PlayerPrefs.DeleteAll();
        pointSystem.CurrentPoints = 0;
        Debug.Log("START GAME");
    }

}
