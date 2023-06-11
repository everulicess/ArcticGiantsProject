using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    public TextMeshProUGUI numberOfLigthsText;
    float maxEnergy = 675f;
    public float currentEnergy;
    public float decreaseRate = 2.25f;
    //float energyDecreaseRateWithLightsOn = 2f;


    GameBehaviour GameManager;

    public Slider EnergyBarSlider;

    private void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        currentEnergy = maxEnergy;
        EnergyBarSlider.maxValue = maxEnergy;
        EnergyBarSlider.value = currentEnergy;
    }

    private void Update()
    {
        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }
        else if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;

        }
        // Decrease Energy faster if lights are on
        //currentEnergy += (/*(energyDecreaseRateWithLightsOn * amountOfLightsOn)*/ +decreaseRate) * Time.deltaTime;
        currentEnergy -= decreaseRate * Time.deltaTime;

        EnergyBarSlider.value = currentEnergy;

        //cahnging the text
        numberOfLigthsText.text = $"{GameManager.lights}/9";
    }
}
