using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

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
        Debug.Log("Started scene");
        currentTime = timeValue;
        scenePoints = FindObjectOfType<PointsCounter>(); // Initialize scenePoints
        pointsSystem = FindObjectOfType<PointsSystem>(); // Initialize pointsSystem
        health = FindObjectOfType<HealthBarSystem>(); // Initialize health
    }

    void Update()
    {
        Debug.Log("Points " + pointsSystem.CurrentPoints);

        

        if (!timerStopped) // Check if the timer is running
        {

            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
            }
            DisplayTime(timeValue);
        }
        
        // Check if player has accumulated enough points for the current scene
        int requiredPoints = GetRequiredPointsForScene(currentScene);
        Debug.Log("Required points " + requiredPoints);
        if (pointsSystem.CurrentPoints < requiredPoints && timeValue > 0 && timeValue < 1)
        {
            Debug.Log("Time value is " + timeValue);

            //Load current scene
            SceneManager.LoadScene(currentScene);
            // If the player hasn't accumulated enough points, reset points for the current scene
            if (currentScene == "Maze Scene 1")
                pointsSystem.CurrentPoints = 0;
            else if (currentScene == "Maze Scene 2")
                pointsSystem.CurrentPoints= 4;
            else if (currentScene == "Maze Scene 3")
                pointsSystem.CurrentPoints = 8;
            else if (currentScene == "Gallery Room 2")
                pointsSystem.CurrentPoints = 4;
            else if (currentScene == "Gallery Room 3")
                pointsSystem.CurrentPoints = 8;

            // extract numeric part from current text and update only the points
            string currentText = scenePoints.pointsText.text;
            int startIndex = currentText.IndexOf(':') + 1; // find the index of ':' and add 1 to get the start index of the numeric part
            string numericPart = currentText.Substring(startIndex).Trim(); // extract and trim the numeric part
            int currentPoints = int.Parse(numericPart); // convert the numeric part to an integer

            // Update the text ui element with the new points
            scenePoints.pointsText.text = currentText.Replace(currentPoints.ToString(), pointsSystem.CurrentPoints.ToString());

        }
        else if(pointsSystem.CurrentPoints == requiredPoints)
        {

            if(requiredPoints > 0)
            {
                SceneManager.LoadScene(GetNextScene(currentScene));
                Debug.Log("Moved to next scene");
            }
            else
            {
                Debug.Log("Else entered");

                if (currentScene == "Maze Scene 1")
                    pointsSystem.CurrentPoints = 0;
                else if (currentScene == "Maze Scene 2")
                    pointsSystem.CurrentPoints = 4;
                else if (currentScene == "Maze Scene 3")
                    pointsSystem.CurrentPoints = 8;
                else if (currentScene == "Gallery Room 2")
                    pointsSystem.CurrentPoints = 4;
                else if (currentScene == "Gallery Room 3")
                    pointsSystem.CurrentPoints = 8;

                // extract numeric part from current text and update only the points
                string currentText = scenePoints.pointsText.text;
                int startIndex = currentText.IndexOf(':') + 1; // find the index of ':' and add 1 to get the start index of the numeric part
                string numericPart = currentText.Substring(startIndex).Trim(); // extract and trim the numeric part
                int currentPoints = int.Parse(numericPart); // convert the numeric part to an integer

                // Update the text ui element with the new points
                scenePoints.pointsText.text = currentText.Replace(currentPoints.ToString(), pointsSystem.CurrentPoints.ToString());

            }
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


    int GetRequiredPointsForScene(string sceneName)
    {
        Debug.Log("Get required points for scene start");
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
        Debug.Log("Getting next scene");

        scenePoints.scenePoints = 0;
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