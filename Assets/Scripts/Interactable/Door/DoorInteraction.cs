using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DoorInteraction : DoorOpening
{
    Camera mainCamera;
    DoorOpening objectHit;
    public bool isInteracting;
    //bool isDoorOpened;

    CharacterMovementHandler characterMovementHandler;

    // Start is called before the first frame update
    void Start()
    {
        
        mainCamera = GetComponentInChildren<Camera>();
        characterMovementHandler = this.GetComponent<CharacterMovementHandler>();

    }
    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    isInteracting = true;
        //    Interacting();
        //    objectHit.isInteracting = true;
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Interacting();
        //}
        if (characterMovementHandler.isPlayerInteracting)
        {

            //Debug.Log("Player is interacting");
            isInteracting = true;
            characterMovementHandler.isPlayerInteracting = false;
        }
        else
        {
            isInteracting = false;
        }
        if (isInteracting)
        {
            Interacting();
            //if (buttonHit)
            //{
                if (isDoorOpened)
                {
                    isDoorOpened = false;
                    objectHit.OpenDoorRPC();
                    //isInteracting = false;
                }
                else
                {
                    isDoorOpened = true;
                    objectHit.CloseDoorRPC();
                    //isInteracting = false;
                }
            //}
            
            
        }
    }
    void Interacting()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            //isInteracting = true;
            if (hit.collider.CompareTag("Door"))
            {
                objectHit = hit.collider.gameObject.GetComponent<DoorOpening>();

                //objectHit.isDoorOpened = !objectHit.isDoorOpened;

                Debug.Log($"DOOR BUTTON HIT {objectHit.isDoorOpened}");

            }
            else
            {
                objectHit = null;
            }
        }
        //void Interacting()
        //{
        //    int x = Screen.width / 2;
        //    int y = Screen.height / 2;
        //    Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y,1));

        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {

        //        if (hit.collider.CompareTag("Door"))
        //        {
        //            objectHit = hit.collider.gameObject.GetComponent<DoorOpening>();

        //            //objectHit.isDoorOpened = !objectHit.isDoorOpened;

        //            Debug.Log($"DOOR BUTTON HIT {objectHit.isDoorOpened}");
        //            //if (objectHit.isDoorOpened == true)
        //            //{
        //            //    objectHit.isDoorOpened = false;
        //            //}
        //            //else
        //            //{
        //            //    objectHit.isDoorOpened = true;
        //            //}
        //        }
        //    }
    }
}
