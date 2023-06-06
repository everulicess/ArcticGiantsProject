using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class DoorOpening : MonoBehaviour
{
    [SerializeField]
    GameObject[] doors;

    [SerializeField]
    Slider energyBar;

    public bool isDoorOpened = false;
    int numberOfDoors;
    private void Update()
    {
        if (isDoorOpened)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void OpenDoor()
    {
        Debug.Log("OpenDoor");
        //isDoorOpened = true;
        numberOfDoors = doors.Length;
        foreach (var doors in doors)
        {
            
            doors.GetComponentInChildren<Animator>().SetBool("openDoor",true);
            doors.GetComponentInChildren<Animator>().Play("ClosingDoor");
        }
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CloseDoor()
    {
        Debug.Log("CloseDoor");
        //isDoorOpened = false;
        foreach (var doors in doors)
        {
            doors.GetComponentInChildren<Animator>().Play("OpeningDoor");
            doors.GetComponentInChildren<Animator>().SetBool("openDoor", false);
            
        }
    }

}
