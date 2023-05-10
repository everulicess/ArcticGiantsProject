using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{

    [Header("Animations")]
    public Animator playerAnimator;
    float walkSpeed = 0f;

    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;
    PickUpObject pickUpObject;

    //Variables
    public bool isPlayerInteracting = false;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        localCamera = GetComponentInChildren<Camera>();
        pickUpObject = GetComponent<PickUpObject>();
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
            //PickUp
            if (networkInputData.isEInteractButtonPressed)
            {
                pickUpObject.PickUp();
            }

            Vector2 walkVector = new Vector2(networkCharacterControllerPrototypeCustom.Velocity.x, networkCharacterControllerPrototypeCustom.Velocity.z);
            walkVector.Normalize();

            walkSpeed = Mathf.Lerp(walkSpeed, Mathf.Clamp01(walkVector.magnitude), Runner.DeltaTime * 5);

            playerAnimator.SetFloat("WalkSpeed", walkSpeed);
            

            if (networkInputData.isFInteractButtonPressed)
            {
                isPlayerInteracting = true;
            }
            else
            {
                isPlayerInteracting = false;
            }
            if (Input.GetKey(KeyCode.Y))
            {
                Debug.Log("Dance");
                playerAnimator.SetFloat("WalkSpeed", 0);
                playerAnimator.SetBool("IsDancePlay", true);
            }
            else
            {
                playerAnimator.SetBool("IsDancePlay", false);

            }


            //Check if we are fallen off the world
            CheckFallRespawn();

            
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
