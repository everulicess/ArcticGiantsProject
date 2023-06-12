using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiresConnected : MonoBehaviour
{
    public int wiresConnected = 0;
    public int ConnectedWires
    {
        get { return wiresConnected; }

        set
        {
            wiresConnected = value;
            Debug.LogFormat("Wires: {0}", wiresConnected);
        }
    }
}
