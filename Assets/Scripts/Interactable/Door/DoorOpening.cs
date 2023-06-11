using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpening : NetworkBehaviour
{
    [SerializeField]
    GameObject[] doors;
    
    EnergyBar energyBar;

    ChangeButtonColor buttonColor;
    [Networked]
    public bool isDoorOpened { get; set; }
    private void Start()
    {
        energyBar = FindObjectOfType<EnergyBar>();
        buttonColor = GetComponentInChildren<ChangeButtonColor>();
    }
    //public bool isInteracting;
    //public bool isPlayerNear; 


    //CheckIfPlayerNear playerNear;

    int numberOfDoors;
   
    //public override void FixedUpdateNetwork()
    //{
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
        //            Debug.Log($"CLOSE {this.gameObject.name} �ldfodishfjdsoifosf");

        //        }
        //        else
        //        {
        //            OpenDoorRPC();
        //            Debug.Log($"OPEN {this.gameObject.name} �ldfodishfjdsoifosf");

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
            //        Debug.Log($"OPEN {this.gameObject.name} �ldfodishfjdsoifosf");
            //    }
            //    else
            //    {
            //        CloseDoorRPC();
            //        Debug.Log($"CLOSE {this.gameObject.name} �ldfodishfjdsoifosf");
            //    }
            
            //}
       // }

    //}
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void OpenDoorRPC()
    {
        buttonColor.ChangeColorRPC("Opened");
        //Decrease 1 per open door
        energyBar.decreaseRate += 1f;
        foreach (var doors in doors)
        {
            //isInteracting = false;
            
            //doors.GetComponentInChildren<Animator>().SetBool("openDoor",true);
            doors.GetComponentInChildren<Animator>().Play("ClosingDoor");
            
            
        }
        //isDoorOpened = false;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void CloseDoorRPC()
    {
        buttonColor.ChangeColorRPC("Closed");
        //Stop consuming energy
        energyBar.decreaseRate -= 1f;
        foreach (var doors in doors)
        {
            //isInteracting = false;
            doors.GetComponentInChildren<Animator>().Play("OpeningDoor");
            //doors.GetComponentInChildren<Animator>().SetBool("openDoor", false);
            
            
        }
        //isDoorOpened = true;
    }

}
