using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public class SettingsScreen : MonoBehaviour
{
    public Toggle fullscreenToggle;

    public List<ResItem> resolutions = new List<ResItem>();// list of resolutions
    public int selectedResolution;

    public TMP_Text resolutionLabel;

    public AudioMixer theMixer;
    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;


    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        bool foundRes = false;
        for(int i = 0; i < resolutions.Count; i++)//check resolutions
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;

                UpdateResLabel();
            }
        }

        float vol = 0f; //set label values to be correct 
        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        theMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();//update volume on screen
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();//update volume on screen
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();//update volume on screen
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()//resolution selection decreases
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void ResRight()//resolution selection increases
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)//if resolution higher than 2 in the array
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();//convert to string
    }
    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenToggle.isOn;

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenToggle.isOn);
    }

    public void SetMasterVol()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();//update volume on screen
        theMixer.SetFloat("MasterVol", masterSlider.value); 
        
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void SetMusicVol()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();//update volume on screen
        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);

    }

    public void SetSFXVol()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();//update volume on screen
        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);

    }
}

[System.Serializable]
public class ResItem//class for resolutions 
{ 
    public int horizontal, vertical;
}

