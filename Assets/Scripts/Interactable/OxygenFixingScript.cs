using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class OxygenFixingScript : MonoBehaviour
{

    [Networked]
    public bool isOxygenBroken { get; set; } = true;
    [Networked]
    public bool oxygenNeedsFixing { get; set; } = false;
    [Networked]
    public bool oxygenIsFixed { get; set; } = false;

    OxygenState oxygenState;
    enum OxygenState
    {
        Broken,
        NeedsFixing,
        FixedAndWorking,
    }
    // Start is called before the first frame update
    void Start()
    {
        oxygenState = OxygenState.Broken;
    }

    // Update is called once per frame
    void Update()
    {
        switch (oxygenState)
        {
            case OxygenState.Broken:
                Broken();
                break;
            case OxygenState.NeedsFixing:
                NeedsFixing();
                break;
            case OxygenState.FixedAndWorking:
                FixedAndWorking();
                break;
            default:
                break;
        }
    }

    //Broken
    void Broken()
    {

        

    }

    //NeedsFixing
    void NeedsFixing()
    {

    }

    //Fixed and Working
    void FixedAndWorking()
    {

    }
}
