using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Questions : MonoBehaviour
{
    public GameObject textBox;

    public GameObject coinObject;
    public GameObject questionObject;
    public PlayerController playerController;
    public Animator playerAnim;


    //whwn player collides with coin, player movement is frozen and question appears
    void OnTriggerEnter(Collider other)
    {
        questionObject.SetActive(true);
        playerController.enabled = false;
        playerAnim.ResetTrigger("Walk");
        playerAnim.SetTrigger("Idle");
    }

    public void EnablePlayerControls()
    {
        // Enable player movement
        playerController.enabled = true;
        // Hide the coin
        coinObject.SetActive(false);
    }

    public void WrongButtonClick()
    {
        // Show try again text
        Debug.Log("WRONG ANSWER. TRY AGAIN!");
        textBox.GetComponent<TMP_Text>().text = "WRONG ANSWER.TRY AGAIN!";
        StartCoroutine(HideTextBoxAfterDelay(3f));
    }

    public void CorrectButtonClick()
    {
        // Show correct text
        Debug.Log("CORRECT ANSWER!");
    }

    private IEnumerator HideTextBoxAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textBox.GetComponent<TMP_Text>().text = "";
    }

}
