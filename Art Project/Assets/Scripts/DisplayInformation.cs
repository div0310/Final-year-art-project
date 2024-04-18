using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInformation : MonoBehaviour
{
    public Image imageUI; 

    void Start()
    {
        // Make sure the image UI is initially disabled
        if (imageUI != null)
        {
            imageUI.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        DisplayInfo();
    }

    void DisplayInfo()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to check if the click is on this GameObject
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                // Toggle the image UI
                if (imageUI != null)
                {
                    imageUI.gameObject.SetActive(!imageUI.gameObject.activeSelf);
                }
            }
        }
    }
}
