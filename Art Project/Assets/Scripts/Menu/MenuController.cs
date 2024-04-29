using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string _newGameLevel;
    public GameObject settingsScreen;
    public GameObject howToPlayScreen;

    public void NewGameLevel()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void OpenSettings()
    {
        settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsScreen.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        howToPlayScreen.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayScreen.SetActive(false);
    }


    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
