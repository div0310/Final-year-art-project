using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    PointsSystem pointSystem;//initialize pointsystem
    HealthBarSystem healthBarSystem;
    HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //reset values at the beginning of the game
        healthBarSystem = FindObjectOfType<HealthBarSystem>();
        pointSystem = FindObjectOfType<PointsSystem>();
        healthBar = FindObjectOfType<HealthBar>();

        PlayerPrefs.DeleteAll();
        pointSystem.CurrentPoints = 0;
        healthBarSystem.CurrentHealth = 100;
        healthBar.SetMaxHealth(healthBarSystem.CurrentHealth);
        healthBar.SetHealth(healthBarSystem.CurrentHealth);

        Debug.Log("START GAME");
        Debug.Log("Healthbarsystem " + healthBarSystem.CurrentHealth);
    }

}
