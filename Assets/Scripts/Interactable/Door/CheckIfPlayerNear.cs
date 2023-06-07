using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerNear : MonoBehaviour
{
    public bool isPlayerNear;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player About to close the door");
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player About to LEAVE");
            isPlayerNear = false;
        }
    }
}
