using Fusion;
using UnityEngine;

public class DoorOpening : NetworkBehaviour
{
    [SerializeField]
    GameObject[] doors;


    [Networked]
    public bool isDoorOpened { get; set; }

    public bool isInteracting = false;
    int numberOfDoors;
    private void Start()
    {
        
    }
    public override void FixedUpdateNetwork()
    {
        if (isInteracting)
        {
            if (isDoorOpened)
            {
                OpenDoorRPC();
                Debug.Log($"OPEN {this.gameObject.name} ñldfodishfjdsoifosf");
            }
            else
            {
                CloseDoorRPC();
                Debug.Log($"CLOSE {this.gameObject.name} ñldfodishfjdsoifosf");
            }
        }
        
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void OpenDoorRPC()
    {
        isInteracting = false;
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
        isInteracting = false;
        //isDoorOpened = true;
        foreach (var doors in doors)
        {
            doors.GetComponentInChildren<Animator>().Play("OpeningDoor");
            //doors.GetComponentInChildren<Animator>().SetBool("openDoor", false);
            
        }
    }

}
