using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    //Lights
    public int numberOfLightsOn = 5;

    //Countdown
    public float startingTime = 20f;
    float currentTime = 0f;
    [SerializeField]
    TMP_Text timerText;

    private void Start()
    {
        //currentTime = startingTime;
    }
    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }

    public int lights
    {
        get { return numberOfLightsOn; }

        set
        {
            numberOfLightsOn = value;
            Debug.LogFormat("items: {0}", numberOfLightsOn);
        }
    }
}
