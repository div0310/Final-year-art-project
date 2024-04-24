using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarSystem : MonoBehaviour
{
    string healthBarKey = "Health";
    public int CurrentHealth
    {
        get; set;
    }

    private void Awake()
    {
        CurrentHealth = PlayerPrefs.GetInt(healthBarKey);
    }

    public void SetPoints(int health)
    {
        PlayerPrefs.SetInt(healthBarKey, health);
    }

    public void Update()
    {
        if (CurrentHealth < 0)
        {
            SceneManager.LoadScene("Game Over Scene");
        }
    }
    //UPDATE METHOD CHECK IF CURRENT POINTS >= 10 
    //PLAY VICTORY SCENE

}
