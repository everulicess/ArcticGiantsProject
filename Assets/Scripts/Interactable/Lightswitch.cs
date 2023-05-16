using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class Lightswitch : NetworkBehaviour
{
    public NetworkObject luz;
    [Networked]
    public bool isLightOn { get; set; } = true;
    bool isNear = false;
    bool isInteracting = false;
    Collider player;


    CharacterMovementHandler characterMovementHandler;

    [Rpc(RpcSources.All, RpcTargets.All)]
    void TurnOnRPC()
    {
        luz.GetComponent<Light>().enabled = true;
        isLightOn = true;
        Debug.Log("Light On");
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    void TurnOffRPC()
    {
        luz.GetComponent<Light>().enabled = false;
        isLightOn = false;
        Debug.Log("Light Off");
    }

    // Update is called once per frame
    void Update()
    {
        if (isNear)
        {
            if (!characterMovementHandler)
            {
                characterMovementHandler = player.GetComponent<CharacterMovementHandler>();
            }

            if (characterMovementHandler.isPlayerInteracting)
            {
                Debug.Log("Player is interacting");
                isInteracting = true;
                characterMovementHandler.isPlayerInteracting = false;
            }
            else
            {
                isInteracting = false;
            }

            if (isInteracting)
            {
                if (isLightOn)
                {
                    TurnOffRPC();
                }
                else
                {
                    TurnOnRPC();
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        player = other;
        if (other.CompareTag("Player"))
        {
            isNear = true;
            Debug.Log("Player is near");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterMovementHandler = null;
            isNear = false;
            Debug.Log("Player is away");
        }
    }

}
