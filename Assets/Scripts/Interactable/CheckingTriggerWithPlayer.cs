using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class CheckingTriggerWithPlayer : NetworkBehaviour
{
    NetworkBool isOnTrigger = false;
    NetworkObject networkObject;

    // Update is called once per frame
    public void Update()
    {
        networkObject = GetComponent<NetworkObject>();
        if (isOnTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Runner.Despawn(networkObject);
            // IDespawned();
            Debug.Log("Package despawned");
        }
    }
    public override void FixedUpdateNetwork()
    {
       
    }

    /*private void IDespawned()
    {
        Runner.Despawn
        Destroy(this.gameObject);
        Debug.Log("IDespawned");
    }*/

  
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            isOnTrigger = true;
            Debug.Log("Is On Trigger True");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isOnTrigger = false;
            Debug.Log("Is On Trigger False");
        }
        

    }
}
