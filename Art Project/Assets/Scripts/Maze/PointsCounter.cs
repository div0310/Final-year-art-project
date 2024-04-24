using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    public TMP_Text pointsText;
    public int scenePoints = 0;
 //SCENE POINTS FOIR THAT SCENE

    private int points = 0;
    PointsSystem pointSystem;//initialize pointsystem

    private void Start()
    {
        pointSystem = FindObjectOfType<PointsSystem>();
        PlayerPrefs.DeleteAll();
        points = pointSystem.CurrentPoints; //save points in integer 

        // Extract the numeric part from the current text and update only the points
        string currentText = pointsText.text;
        int startIndex = currentText.IndexOf(':') + 1; // Find the index of ':' and add 1 to get the start index of the numeric part
        string numericPart = currentText.Substring(startIndex).Trim(); // Extract and trim the numeric part
        int currentPoints = int.Parse(numericPart); // Convert the numeric part to an integer

        // Update the TextMeshProUGUI element with the updated points
        pointsText.text = currentText.Replace(currentPoints.ToString(), points.ToString());
    }
    public void UpdatePointsText()
    {
        points++;
        // Extract the numeric part from the current text and update only the points
        string currentText = pointsText.text;
        int startIndex = currentText.IndexOf(':') + 1; // Find the index of ':' and add 1 to get the start index of the numeric part
        string numericPart = currentText.Substring(startIndex).Trim(); // Extract and trim the numeric part
        int currentPoints = int.Parse(numericPart); // Convert the numeric part to an integer

        // Update the TextMeshProUGUI element with the updated points
        pointsText.text = currentText.Replace(currentPoints.ToString(), points.ToString());

        pointSystem.SetPoints(points);//updates value in points system
    }

    
}