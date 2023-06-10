using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class CharacterMovementHandler : NetworkBehaviour
{

    [Header("Animations")]
    public Animator playerAnimator;
    float walkSpeed = 0f;

    bool isRespawnRequested = false;
    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;
    PickUpObject pickUpObject;

    GameBehaviour gameManager;

    //Variables
    public bool isPlayerInteracting = false;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "New modeled")
        {
            gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
            pickUpObject = GetComponent<PickUpObject>();

        }

        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        localCamera = GetComponentInChildren<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if (isRespawnRequested)
        {
            Respawn();
        }
        //Get Input from network
        if (GetInput(out NetworkInputData networkInputData))
        {
            if (SceneManager.GetActiveScene().name == "New modeled")
            {
            //if (gameManager.isPlayerControl)
            //{

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

                if (networkInputData.isEInteractButtonPressed)
                {
                    pickUpObject.Update();
                }
                if (networkInputData.isFInteractButtonPressed)
                {
                    isPlayerInteracting = true;
                }
                else
                {
                    isPlayerInteracting = false;
                }

                Vector2 walkVector = new Vector2(networkCharacterControllerPrototypeCustom.Velocity.x, networkCharacterControllerPrototypeCustom.Velocity.z);
                walkVector.Normalize();

                walkSpeed = Mathf.Lerp(walkSpeed, Mathf.Clamp01(walkVector.magnitude), Runner.DeltaTime * 5);

                playerAnimator.SetFloat("WalkSpeed", walkSpeed);

            //}
                //PickUp
                
                
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
            transform.position = Utils.GetFirstSpawnPoint("first");
        }
    }

    public void RequestRespawn()
    {
        isRespawnRequested = true;
    }

    void Respawn()
    {
        networkCharacterControllerPrototypeCustom.TeleportToPosition(Utils.GetFirstSpawnPoint("First"));

        isRespawnRequested = false;
    }

    public void SetCharacterControllerEnabled(bool isEnabled)
    {
        networkCharacterControllerPrototypeCustom.Controller.enabled = isEnabled;
    }

}
