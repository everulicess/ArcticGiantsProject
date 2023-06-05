using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class CheckingTriggerWithPlayer : NetworkBehaviour
{
    public bool isPackageReady = false;  
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Package"))
        {
            isPackageReady = true;
            Debug.Log("Package On Site");
        }
        
    }
}
