using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SolarPanels : NetworkBehaviour
{
    SolarPanelState solarPanelState;
    WiresConnected wiresConnected;
    [SerializeField]
    EnergyBar energyBar;
    private void Start()
    {
        solarPanelState = SolarPanelState.ConnectWires;
        wiresConnected = this.gameObject.GetComponent<WiresConnected>();
    }
    enum SolarPanelState
    {
        ConnectWires,
        ProducingEnergy
    }
    public override void FixedUpdateNetwork()
    {
        switch (solarPanelState)
        {
            case SolarPanelState.ConnectWires:
                ConnectWires();
                break;
            case SolarPanelState.ProducingEnergy:
                ProducingEnergy();
                break;
            default:
                break;
        }
    }

    void ConnectWires()
    {
        if (wiresConnected.ConnectedWires == 4)
        {
            solarPanelState = SolarPanelState.ProducingEnergy;
        }

    }

    void ProducingEnergy()
    {
        //less rate means something is being produced
        energyBar.decreaseRate -= 0.75f;
    }
}
