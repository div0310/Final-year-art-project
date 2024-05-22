using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsScreen;
    public GameObject infoScreen;

    private void Awake()
    {
        infoScreen.SetActive(true);
        Time.timeScale = 0f;// timer is frozen
        gameIsPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;// game restarts
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    //function that unfreezes game after continue button is clicked
    public void CloseInfoScreen()
    {
        infoScreen.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();

    }
}
