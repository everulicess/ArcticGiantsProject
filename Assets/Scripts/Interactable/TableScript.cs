using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class TableScript : MonoBehaviour
{
    [SerializeField]
    GameObject timer;

    TableState tableState;

    [Networked]
    public bool isBroken { get; set; } = true;
    [Networked]
    public bool needsFixing { get; set; } = false;
    [Networked]
    public bool isFixed { get; set; } = false;
    [Networked]
    public bool sendingSignals { get; set; } = false;
    enum TableState
    {
        Broken,
        NeedsFixing,
        Fixed,
        SendingSignals
    }

    
    // Start is called before the first frame update
    void Start()
    {
        tableState = TableState.Broken;
    }

    // Update is called once per frame
    void Update()
    {
        switch (tableState)
        {
            case TableState.Broken:
                BrokenTable();
                ;break; 
            case TableState.NeedsFixing:
                NeedsFixing();
                ; break;
            case TableState.Fixed:
                Fixed();
                ; break;
            case TableState.SendingSignals:
                SendingSignals();
                ; break;
        }
    }
    void BrokenTable()
    {
        Debug.Log("StateBrokenTable");
        needsFixing = true;
    }
    void NeedsFixing()
    {
        Debug.Log("StateNeedsFixingTable");
        isFixed = true;
    }
    void Fixed()
    {
        Debug.Log("StateFixedTable");
        sendingSignals = true;
    }
    void SendingSignals()
    {
        Debug.Log("StateSendingTable");
        timer.SetActive(true);
        timer.GetComponent<Timer>().StartTimer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isBroken)
            {
                
                if (needsFixing)
                {
                    tableState = TableState.NeedsFixing;
                    if (isFixed)
                    {
                        tableState = TableState.Fixed;
                        if (sendingSignals)
                        {
                            tableState = TableState.SendingSignals;
                        }
                    }
                }
            }
            
            
        }
    }
}
