using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class OxygenFixingScript : MonoBehaviour
{

    GameBehaviour GameManager;

    //player near
    bool isPlayerNear = false;

    [Networked]
    public bool oxygenNeedsFixing { get; set; } = false;
    [Networked]
    public bool oxygenIsFixed { get; set; } = false;

    OxygenState oxygenState;
    enum OxygenState
    {
        NeedsFixing,
        FixedAndWorking,
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        oxygenState = OxygenState.NeedsFixing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (oxygenState)
        {
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


    //NeedsFixing
    void NeedsFixing()
    {
        //show what is needed to fix the oxygen into the UI
        if (isPlayerNear)
        {
            if (GameManager.wires >= 3 && GameManager.pliers >= 1 && GameManager.screwdriver >= 1)
            {
                Debug.Log("Fixing oxygen");
                oxygenState = OxygenState.FixedAndWorking;
            }
        }
        
    }

    //Fixed and Working
    void FixedAndWorking()
    {
        GameManager.isOxygenFixed = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log($"{isPlayerNear} oxygen");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log($"{isPlayerNear} no oxygen");
        }
    }
}
