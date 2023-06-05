using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{
    public TextMeshProUGUI numberOfLigthsText;
    float maxEnergy = 1000f;
    public float currentEnergy;
    float decreaseRate = 1f;
    float energyDecreaseRateWithLightsOn = 2f;


    GameBehaviour GameManager;
    int amountOfLightsOn;

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
        amountOfLightsOn = GameManager.lights;
            // Decrease Energy faster if lights are on
            currentEnergy -= ((energyDecreaseRateWithLightsOn * amountOfLightsOn) + decreaseRate) * Time.deltaTime;
        
        EnergyBarSlider.value = currentEnergy;

        // Check if out of Energy
        if (currentEnergy <= 0)
        {
            // TODO: Handle out of Energy situation
        }
        //cahnging the text
        numberOfLigthsText.text = $"{GameManager.lights}/5 Lights On";
    }
}
