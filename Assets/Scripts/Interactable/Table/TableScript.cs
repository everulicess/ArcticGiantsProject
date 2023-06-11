using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TableScript : NetworkBehaviour
{
    [SerializeField]
    GameObject timer;

    //particles
    [SerializeField]
    ParticleSystem damagedParticleSystem;

    GameBehaviour GameManager;


    TableState tableState;
    [SerializeField]
    EnergyBar energyBar;
    
    public bool tableNeedsFixing { get; set; } = true;
    [Networked]
    public bool TableIsFixed { get; set; } = false;
    enum TableState
    {
        NeedsFixing,
        Fixed,
        SendingSignals
    }

    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        tableState = TableState.NeedsFixing;
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        switch (tableState)
        {
            case TableState.NeedsFixing:
                NeedsFixing();
                Debug.Log("Increasing energy because it's broken");
                energyBar.decreaseRate += 0.75f;
                ; break;
            case TableState.Fixed:
                Fixed();
                ; break;
            case TableState.SendingSignals:
                SendingSignals();
                ; break;
            default:
                ; break;
        }
    }
    void NeedsFixing()
    {
        damagedParticleSystem.Play();
        if (isPlayerNear)
        {
            CheckTools();
        }
    }

    void Fixed()
    {
        if (isPlayerNear)
        {
            CheckId();
        }
    }
    void SendingSignals()
    {
        GameManager.isTableFixed = true;
        damagedParticleSystem.Stop();
        timer.SetActive(true);
    }
    //CheckID
    void CheckId()
    {
        Debug.Log("Checking ID");
        if (GameManager.hasID)
        {
            tableState = TableState.SendingSignals;
        }
    }
    //CheckTools
    void CheckTools()
    {
        Debug.Log("Checking Tools for repairing");
        int wiresNum = 2; int wrenchNum = 1; int pliersNum = 1; int screwdriverNum = 1;
        if (GameManager.wires >= wiresNum && GameManager.wrench >= wrenchNum && GameManager.pliers >= pliersNum && GameManager.screwdriver >= screwdriverNum )
        {
            GameManager.wires -= wiresNum;
            GameManager.wrench -= wrenchNum;
            GameManager.pliers -= pliersNum;
            GameManager.screwdriver -= screwdriverNum;

            tableState = TableState.Fixed;

            damagedParticleSystem.Stop();
            Debug.Log("StateFixedTable");
        }
    }

    bool isPlayerNear = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log($"{isPlayerNear} no oxygen");
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
