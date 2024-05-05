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
    public string currentScene;
    public string nextScene;
    public int requiredPoints;

    private bool timerStopped = false; // Flag to indicate if the timer has been stopped

    void Start()
    {
        Debug.Log("Started scene");
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
            }
            DisplayTime(timeValue);
        }

        if (timeValue > 0 && timeValue < 1)
        {

            Debug.Log("Points " + pointsSystem.CurrentPoints);
            Debug.Log("Required points " + requiredPoints);

            if (pointsSystem.CurrentPoints < requiredPoints)
            {

                //Load current scene
                SceneManager.LoadScene(currentScene);
                Debug.Log("Reloaded scene");

                // If the player hasn't accumulated enough points, reset points for the current scene
                if (currentScene == "Maze Scene 1")
                {
                    pointsSystem.CurrentPoints = 0;
                    pointsSystem.SetPoints(pointsSystem.CurrentPoints);
                }
                else if (currentScene == "Maze Scene 2")
                {
                    pointsSystem.CurrentPoints = 4;
                    pointsSystem.SetPoints(pointsSystem.CurrentPoints);
                }
                else if (currentScene == "Maze Scene 3")
                {
                    pointsSystem.CurrentPoints = 8;
                    pointsSystem.SetPoints(pointsSystem.CurrentPoints);
                }

                // extract numeric part from current text and update only the points
                string currentText = scenePoints.pointsText.text;
                int startIndex = currentText.IndexOf(':') + 1; // find the index of ':' and add 1 to get the start index of the numeric part
                string numericPart = currentText.Substring(startIndex).Trim(); // extract and trim the numeric part
                int currentPoints = int.Parse(numericPart); // convert the numeric part to an integer

                // Update the text ui element with the new points
                scenePoints.pointsText.text = currentText.Replace(currentPoints.ToString(), pointsSystem.CurrentPoints.ToString());

            }
            else if (pointsSystem.CurrentPoints >= requiredPoints)
            {
                SceneManager.LoadScene(nextScene);
                Debug.Log("Moved to next scene");
            }

        }

        if (pointsSystem.CurrentPoints == requiredPoints && requiredPoints > 0)
        {
            Debug.Log("Points " + pointsSystem.CurrentPoints);
            Debug.Log("Required points " + requiredPoints);

            SceneManager.LoadScene(nextScene);
            Debug.Log("Moved to next scene");



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

}