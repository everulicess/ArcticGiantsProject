using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TableScript : NetworkBehaviour
{
    [SerializeField]
    GameObject timer;

    //repair Icon
    [SerializeField]
    GameObject repairIcon;

    [SerializeField]
    float iconHeight = 2.0f;

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

        if (repairIcon != null)
        {
            repairIcon.transform.position = transform.position + new Vector3(0, iconHeight, 0);
        }
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        switch (tableState)
        {
            case TableState.NeedsFixing:
                Debug.Log($"Increasing energy because it's broken   {energyBar.decreaseRate}");
                energyBar.decreaseRateTable = 0.75f;
                NeedsFixing();
                ; break;
            case TableState.Fixed:
                energyBar.decreaseRateTable = 0.25f;
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
            if (Input.GetKey(KeyCode.F))
            {
                CheckTools();
            }
            
        }
    }

    void Fixed()
    {
        if (isPlayerNear)
        {
            if (Input.GetKey(KeyCode.F))
            {
                CheckId();
            }
            
        }
    }
    void SendingSignals()
    {
        GameManager.isTableFixed = true;
        
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
        int wiresNum = 3; int wrenchNum = 0; int pliersNum = 1; int screwdriverNum = 0;
        if (GameManager.wires >= wiresNum && GameManager.wrench >= wrenchNum && GameManager.pliers >= pliersNum && GameManager.screwdriver >= screwdriverNum )
        {
            GameManager.wires -= wiresNum;
            GameManager.wrench -= wrenchNum;
            GameManager.pliers -= pliersNum;
            GameManager.screwdriver -= screwdriverNum;

            tableState = TableState.Fixed;

            damagedParticleSystem.Stop();
            Debug.Log("StateFixedTable");

            if (repairIcon != null)
            {
                repairIcon.SetActive(false);
            }
        }
    }

    bool isPlayerNear = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
       
    }
}
