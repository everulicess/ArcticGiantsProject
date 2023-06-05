using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OxygenBar : MonoBehaviour
{
    float maxOxygen = 10000f;
    public float currentOxygen;
    float decreaseRate = 20f;


    GameBehaviour GameManager;

    public Slider oxygenBarSlider;

    private void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        currentOxygen = maxOxygen;
        oxygenBarSlider.maxValue = maxOxygen;
        oxygenBarSlider.value = currentOxygen;
    }

    private void Update()
    {
        currentOxygen -= (decreaseRate) * Time.deltaTime;
        oxygenBarSlider.value = currentOxygen;

        
    }
}
