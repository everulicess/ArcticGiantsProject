using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    public int numberOfLightsOn = 5;

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
