using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoutdownTimer : MonoBehaviour
{
    float currentTime = 120f;
    public Text timeText;

    void Update()
    {
        if(currentTime > 0)
        {
        currentTime -= Time.deltaTime;
        } 
        else 
        {
            currentTime = 0;

        }
        DisplayTime(currentTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
