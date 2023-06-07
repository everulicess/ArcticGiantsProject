using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class DoorOpening : NetworkBehaviour
{
    [SerializeField]
    GameObject[] doors;

    [SerializeField]
    Slider energyBar;

    [Networked]
    public bool isDoorOpened { get; set; }
    int numberOfDoors;
    private void Update()
    {
        if (isDoorOpened)
        {
            CloseDoorRPC();
            Debug.Log($"CLOSE {this.gameObject.name} ñldfodishfjdsoifosf");
        }
        else
        {
            OpenDoorRPC();
            Debug.Log($"OPEN {this.gameObject.name} ñldfodishfjdsoifosf");
        }
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void OpenDoorRPC()
    {
        Debug.Log("OpenDoor");
        //isDoorOpened = false;
        numberOfDoors = doors.Length;
        foreach (var doors in doors)
        {
            
            //doors.GetComponentInChildren<Animator>().SetBool("openDoor",true);
            doors.GetComponentInChildren<Animator>().Play("ClosingDoor");
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CloseDoorRPC()
    {
        Debug.Log("CloseDoor");
        //isDoorOpened = true;
        foreach (var doors in doors)
        {
            doors.GetComponentInChildren<Animator>().Play("OpeningDoor");
            //doors.GetComponentInChildren<Animator>().SetBool("openDoor", false);
            
        }
    }

}
