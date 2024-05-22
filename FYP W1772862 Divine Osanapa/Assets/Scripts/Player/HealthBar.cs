using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //script for healthbar visuals
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    HealthBarSystem healthBarSystem;

    private void Start()
    {
        healthBarSystem = FindObjectOfType<HealthBarSystem>();
        SetHealth(healthBarSystem.CurrentHealth);

    }

   
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        gradient.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

        healthBarSystem.SetHealth(health);

        Debug.Log("Fill color " + slider.normalizedValue);
    }
}
