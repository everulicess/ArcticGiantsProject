using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class CheckingTriggerWithPlayer : NetworkBehaviour
{
    public bool isPackageReady = false;
    
    // Update is called once per frame
    public void Update()
    {
    }

  
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Package"))
        {
            Debug.Log("Package On Site");
        }
        
    }
}
