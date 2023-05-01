using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class Lightswitch : NetworkBehaviour
{
    public NetworkObject luz;
    NetworkBool isLightOn;
    bool isNear = false;
    bool isInteracting = false;
    Collider player;


    CharacterMovementHandler characterMovementHandler;

    
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
                    TurnOff();
                }
                else
                {
                    TurnOn();
                }
            }
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
    //Intenta usar esto en el script de Player, en la parte de hasstateauthority a lo mejor funciona
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

    void TurnOn()
    {
            luz.gameObject.SetActive(true);
            isLightOn = true;
            Debug.Log("Light On");  
    }

    void TurnOff()
    {
            luz.gameObject.SetActive(false);
            isLightOn = false;
            Debug.Log("Light Off");    
    }
    
    
    
}
