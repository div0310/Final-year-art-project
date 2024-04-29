using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PaintingInfo
{
    public GameObject paintingObject;
    public Image imageUI;
    public TMP_Text[] hintTexts; // Array of hint texts
    public KeyCode displayKey = KeyCode.E;
    public float range = 2f; 
    public bool playerInRange = false;
}

public class DisplayInformation : MonoBehaviour
{
    public PaintingInfo[] paintings;

    void Start()
    {
        foreach (PaintingInfo painting in paintings)
        {
            //Check the image UI is initially disabled
            if (painting.imageUI != null)
            {
                painting.imageUI.gameObject.SetActive(false);
            }
            // Disable hint texts initially
            foreach (TMP_Text hintText in painting.hintTexts)
            {
                if (hintText != null)
                {
                    hintText.gameObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        foreach (PaintingInfo painting in paintings)
        {
            // Check if the player is in range of this painting and display the hint text
            if (painting.playerInRange)
            {
                foreach (TMP_Text hintText in painting.hintTexts)
                {
                    if (hintText != null)
                    {
                        hintText.gameObject.SetActive(true);
                    }
                }

                // Check if the display key is pressed
                if (Input.GetKeyDown(painting.displayKey))
                {
                    // Toggle image UI
                    if (painting.imageUI != null)
                    {
                        painting.imageUI.gameObject.SetActive(!painting.imageUI.gameObject.activeSelf);
                    }
                }
            }
            else
            {
                foreach (TMP_Text hintText in painting.hintTexts)
                {
                    if (hintText != null)
                    {
                        hintText.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (PaintingInfo painting in paintings)
        {
            if (other.CompareTag("Player") && other.gameObject == painting.paintingObject)
            {
                painting.playerInRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (PaintingInfo painting in paintings)
        {
            if (other.CompareTag("Player") && other.gameObject == painting.paintingObject)
            {
                painting.playerInRange = false;
            }
        }
    }
}