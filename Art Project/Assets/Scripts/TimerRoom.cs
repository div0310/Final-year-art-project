using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
//using UnityEditor.SearchService;

public class TimerRoom : MonoBehaviour
{
    public float timeValue = 180f; // Total time in seconds
    public TMP_Text timeText;
    private float currentTime;
    //public string nextScene;
    public string currentScene;


    void Start()
    {
        currentTime = timeValue;
    }

    void Update()
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

    void DisplayTime(float timeToDisplay)
    {
        if( timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    void LoadNextScene()
    {
        if (currentScene == "Gallery Room 1")
        {
            SceneManager.LoadScene("Maze Scene 1");
        }
        else if(currentScene == "Gallery Room 2")
        {
            SceneManager.LoadScene("Maze Scene 2");

        }
        else if(currentScene == "Gallery Room 3")
        {
            SceneManager.LoadScene("Maze Scene 3");

        }
        else if(currentScene == "Maze Scene 1")
        {
            SceneManager.LoadScene("Gallery Room 2");
        }
        //else
        //{
        //    return;
        //}
        //SceneManager.LoadScene("Maze Scene");
        // Load the next scene when the timer is up
        //SceneManager.LoadScene(scene);
    }
}