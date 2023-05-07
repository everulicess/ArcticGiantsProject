using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityManager : MonoBehaviour
{
    public Slider ElectricityBar;
    public float MaxElectricity = 100; // maximum electricity value
    private float currentElectricity = 100; // current electricity value
    public float electricityDecreaseRateOn = 0.5f; // rate at which the electricity decreases when lights are on
    public float electricityDecreaseRateOff = 0.1f; // rate at which the electricity decreases when lights are off
    private Lightswitch lightswitch;
    //public Game_Manager gameManager;
    float lightDecrease = 1f;





    private void Start()
    {
        ElectricityBar.minValue = 0;
        ElectricityBar.maxValue = MaxElectricity;
        ElectricityBar.value = currentElectricity;
        lightswitch = GetComponent<Lightswitch>();

        //gameManager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
    }

    private void FixedUpdate()
    {
        ElectricityBar.value = currentElectricity;

    }

    void Update()
    {
        //int lightsCount = gameManager.lights;

        currentElectricity -= ((lightDecrease  /*lightsCount*/) + electricityDecreaseRateOff) * Time.deltaTime;


        // Clamp current electricity value between 0 and maxElectricity
        currentElectricity = Mathf.Clamp(currentElectricity, 0f, MaxElectricity);
    }
}
