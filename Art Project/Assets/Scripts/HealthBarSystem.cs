using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarSystem : MonoBehaviour
{
    //healthbar system to keep track of health between scenes
    string healthBarKey = "Health";
    public int CurrentHealth
    {
        get; set;
    }

    private void Awake()
    {
        CurrentHealth = PlayerPrefs.GetInt(healthBarKey);
    }

    public void SetHealth(int health)
    {
        PlayerPrefs.SetInt(healthBarKey, health);
       
    }


}
