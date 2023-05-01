using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{ 
    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;

    //Variables
    public bool isPlayerInteracting = false;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        localCamera = GetComponentInChildren<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        //Get Input from network
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Rotate the transform according to the client
            transform.forward = networkInputData.aimForwardVector;

            //cancel out rotation on x axis so character doesn't tilt
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            //move
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            //Jump
            if (networkInputData.isJumpButtonPressed)
            {
                networkCharacterControllerPrototypeCustom.Jump();
            }

            //Check if we are fallen off the world
            CheckFallRespawn();

            if (networkInputData.isInteractButtonPressed)
            {
                isPlayerInteracting = true;
            }
        }
    }


    void CheckFallRespawn()
    {
        if (transform.position.y < -5)
        {
            Debug.Log("Respawning on the plane");
            transform.position = Utils.GetRandomSpawnPoint();
        }
    }



}
