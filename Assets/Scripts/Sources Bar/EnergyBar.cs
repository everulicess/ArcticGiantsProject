using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    float maxEnergy = 100f;
    float currentEnergy;
    float decreaseRate = 1f;
    bool lightsOn = false;
    float energyDecreaseRateWithLightsOn = 2f;

    public Slider EnergyBarSlider;

    private void Start()
    {
        currentEnergy = maxEnergy;
        EnergyBarSlider.maxValue = maxEnergy;
        EnergyBarSlider.value = currentEnergy;
    }

    private void Update()
    {
        if (lightsOn)
        {
            // Decrease Energy faster if lights are on
            currentEnergy -= energyDecreaseRateWithLightsOn * Time.deltaTime;
        }
        else
        {
            currentEnergy -= decreaseRate * Time.deltaTime;
        }
        EnergyBarSlider.value = currentEnergy;

        // Check if out of Energy
        if (currentEnergy <= 0)
        {
            // TODO: Handle out of Energy situation
        }
    }
}
