using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using UnityEngine.UI;
using TMPro;

public class Lightswitch : NetworkBehaviour
{
    public NetworkObject luz;
    CharacterMovementHandler characterMovementHandler;

    //number of lights on
    GameBehaviour GameManager;

    bool isNear = false;
    bool isInteracting = false;
    Collider player;

    EnergyBar energyBar;
    public TextMeshPro interactText;
    [Networked]
    public bool isLightOn { get; set; } = true;

    private void Start()
    {
        energyBar = FindObjectOfType<EnergyBar>();
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        interactText.enabled = false;
    }
    // Update is called once per frame
    public override void FixedUpdateNetwork()
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
            interactText.enabled = true;
            isNear = true;
            Debug.Log("Player is near");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.enabled = false;
            characterMovementHandler = null;
            isNear = false;
            Debug.Log("Player is away");
        }
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    void TurnOnRPC()
    {
        energyBar.decreaseRate += 0.25f;
        Debug.Log($"{this.gameObject.name} is CONSUMING {energyBar.decreaseRate} energy");

        GameManager.lights += 1;
        luz.GetComponent<Light>().enabled = true;
        isLightOn = true;
        //Debug.Log("Light On");
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    void TurnOffRPC()
    {
        energyBar.decreaseRate -= 0.25f;
        Debug.Log($"{this.gameObject.name} is not consuming energy");
        GameManager.lights -= 1;
        luz.GetComponent<Light>().enabled = false;
        isLightOn = false;
        //Debug.Log("Light Off");
    }

}
