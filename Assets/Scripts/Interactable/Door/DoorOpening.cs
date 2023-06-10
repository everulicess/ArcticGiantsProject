using Fusion;
using UnityEngine;

public class DoorOpening : NetworkBehaviour
{
    [SerializeField]
    GameObject[] doors;


    [Networked]
    public bool isDoorOpened { get; set; }

    //public bool isInteracting;
    //public bool isPlayerNear; 
    

    //CheckIfPlayerNear playerNear;

    int numberOfDoors;
    private void Start()
    {
    }
    public override void FixedUpdateNetwork()
    {
        //if (isPlayerNear)
        //{
        //    if (!characterMovementHandler)
        //    {
        //        characterMovementHandler = player.GetComponent<CharacterMovementHandler>();
        //    }

        //    if (characterMovementHandler.isPlayerInteracting)
        //    {

        //        Debug.Log("Player is interacting");
        //        isInteracting = true;
        //        characterMovementHandler.isPlayerInteracting = false;
        //    }
        //    else
        //    {
        //        isInteracting = false;
        //    }
        //    if (isInteracting)
        //    {

        //        if (isDoorOpened)
        //        {
        //            CloseDoorRPC();
        //            Debug.Log($"CLOSE {this.gameObject.name} ñldfodishfjdsoifosf");

        //        }
        //        else
        //        {
        //            OpenDoorRPC();
        //            Debug.Log($"OPEN {this.gameObject.name} ñldfodishfjdsoifosf");

        //        }
        //    }
        //}
        //if (playerNear.isPlayerNear)
        //{
            //if (isInteracting)
            //{
            //    isDoorOpened = !isDoorOpened;
            //    if (isDoorOpened)
            //    {
            //        OpenDoorRPC();
            //        Debug.Log($"OPEN {this.gameObject.name} ñldfodishfjdsoifosf");
            //    }
            //    else
            //    {
            //        CloseDoorRPC();
            //        Debug.Log($"CLOSE {this.gameObject.name} ñldfodishfjdsoifosf");
            //    }
            
            //}
       // }

    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void OpenDoorRPC()
    {
        
        foreach (var doors in doors)
        {
            //isInteracting = false;
            
            //doors.GetComponentInChildren<Animator>().SetBool("openDoor",true);
            doors.GetComponentInChildren<Animator>().Play("ClosingDoor");
            
            
        }
        isDoorOpened = false;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CloseDoorRPC()
    {
        
        foreach (var doors in doors)
        {
            //isInteracting = false;
            doors.GetComponentInChildren<Animator>().Play("OpeningDoor");
            //doors.GetComponentInChildren<Animator>().SetBool("openDoor", false);
            
            
        }
        isDoorOpened = true;
    }

}
