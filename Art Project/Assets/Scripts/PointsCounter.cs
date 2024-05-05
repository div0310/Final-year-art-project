using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    public TMP_Text pointsText;

    public int points;
    PointsSystem pointSystem;//initialize pointsystem

    private void Start()
    {
        pointSystem = FindObjectOfType<PointsSystem>();
        points = pointSystem.CurrentPoints; //save points in integer 

        // extract the numeric part from current text and update only the points
        string currentText = pointsText.text;
        int startIndex = currentText.IndexOf(':') + 1; // find index of ':' and add 1 to get the start index of the numeric part
        string numericPart = currentText.Substring(startIndex).Trim(); // extract and trim the numeric part
        int currentPoints = int.Parse(numericPart); // convert numeric part to an integer

        // Update the text ui element with the new points
        pointsText.text = currentText.Replace(currentPoints.ToString(), points.ToString());

        Debug.Log("START POINT COUNTER Points " + points);
        Debug.Log("START POINT COUNTER Current Points " + pointSystem.CurrentPoints);
    }
    public void UpdatePointsText()
    {
        points++;
        // extract numeric part from current text and update only the points
        string currentText = pointsText.text;
        int startIndex = currentText.IndexOf(':') + 1; // find the index of ':' and add 1 to get the start index of the numeric part
        string numericPart = currentText.Substring(startIndex).Trim(); // extract and trim the numeric part
        int currentPoints = int.Parse(numericPart); // convert the numeric part to an integer

        // Update the text ui element with the new points
        pointsText.text = currentText.Replace(currentPoints.ToString(), points.ToString());

        //pointSystem.SetPoints(points);//updates value in points system
        pointSystem.CurrentPoints = points;
        pointSystem.SetPoints(points);

        Debug.Log("UPDATE POINT COUNTER Points " + points);
        Debug.Log("UPDATE POINT COUNTER Current Points " + pointSystem.CurrentPoints);
    }


}