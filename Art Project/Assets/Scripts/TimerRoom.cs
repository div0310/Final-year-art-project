using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerRoom : MonoBehaviour
{
    //INITIALIZE POINTSYSTEM , POINTS COUNTER AND HEALTHBAR SYSTEM 
    private PointsCounter scenePoints;
    private PointsSystem pointsSystem;
    private HealthBarSystem health;
    public float timeValue = 180f; // Total time in seconds
    public TMP_Text timeText;
    private float currentTime;
    public string currentScene;

    private bool timerStopped = false; // Flag to indicate if the timer has been stopped

    void Start()
    {
        currentTime = timeValue;
        scenePoints = FindObjectOfType<PointsCounter>(); // Initialize scenePoints
        pointsSystem = FindObjectOfType<PointsSystem>(); // Initialize pointsSystem
        health = FindObjectOfType<HealthBarSystem>(); // Initialize health
    }

    void Update()
    {
        if (!timerStopped) // Check if the timer is running
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
                LoadNextScene();
                //change scene
            }
            DisplayTime(timeValue);
        }
    }

    //FUNCTION that shows time using ui
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LoadNextScene()
    {
        // Stop the timer
        timerStopped = true;

        // Check if player has accumulated enough points for the current scene
        int requiredPoints = GetRequiredPointsForScene(currentScene);

        if (scenePoints.scenePoints < requiredPoints)
        {
            // If the player hasn't accumulated enough points, reset points for the current scene
            if (currentScene == "Maze Scene 1")
                pointsSystem.SetPoints(0);
            else if (currentScene == "Maze Scene 2")
                pointsSystem.SetPoints(4);
            else if (currentScene == "Maze Scene 3")
                pointsSystem.SetPoints(8);

            // Load the current scene
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            // If the player has enough points, reset points for the next scene
            int nextSceneRequiredPoints = GetRequiredPointsForScene(GetNextScene(currentScene));
            pointsSystem.SetPoints(nextSceneRequiredPoints);

            // Load the next scene
            SceneManager.LoadScene(GetNextScene(currentScene));
        }
    }

    int GetRequiredPointsForScene(string sceneName)
    {
        // Define the required points for each scene
        switch (sceneName)
        {
            case "Maze Scene 1":
                return 4;
            case "Maze Scene 2":
                return 8;
            case "Maze Scene 3":
                return 14;
            default:
                return 0; // Default to 0 points if scene not recognized
        }
    }

    string GetNextScene(string currentScene)
    {
        // Define the next scene for each current scene
        switch (currentScene)
        {
            case "Gallery Room 1":
                return "Maze Scene 1";
            case "Gallery Room 2":
                return "Maze Scene 2";
            case "Gallery Room 3":
                return "Maze Scene 3";
            case "Maze Scene 1":
                return "Gallery Room 2";
            case "Maze Scene 2":
                return "Gallery Room 3";
            case "Maze Scene 3":
                return "Win Scene";
            default:
                return "Game Over Scene"; // Default to "Game Over Scene" if current scene not recognized
        }
    }
}