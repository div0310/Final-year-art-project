using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour
{
    public GameObject infoUI;
    public GameObject hintText;
    private bool canInteract = false;
    private bool isUIVisible = false;

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            isUIVisible = !isUIVisible; // toggle info ui
            infoUI.SetActive(isUIVisible);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        hintText.SetActive(true);
        canInteract = true;
    }

    void OnTriggerExit(Collider other)
    {
        hintText.SetActive(false);
        canInteract = false;
    }
}