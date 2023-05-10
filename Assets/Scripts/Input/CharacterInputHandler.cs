using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;
    bool isJumpButtonPressed = false;
    bool isFInteractButtonPressed = false;
    bool isEInteractButtonPressed = false;

    //other component
    LocalCameraHandler localCameraHandler;
    CharacterMovementHandler characterMovementHandler;
    private void Awake()
    {
        localCameraHandler = GetComponentInChildren<LocalCameraHandler>();
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority)
            return;

        //view Input
        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y") *-1; //Invert the mouse look

        //Move Input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        //Jump
        if(Input.GetButtonDown("Jump"))
        {
            isJumpButtonPressed = true;
        }
        //Interact "F"
        if(Input.GetKeyDown(KeyCode.F))
        {
            isFInteractButtonPressed = true;
        } 
        //Interact "E"
        if(Input.GetKeyDown(KeyCode.E))
        {
            isEInteractButtonPressed = true;
        }

        //Set View
        localCameraHandler.SetViewInputVector(viewInputVector);
    }

    public NetworkInputData GetNetworkInputData()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Aim Data
        networkInputData.aimForwardVector = localCameraHandler.transform.forward;

        //Move Data
        networkInputData.movementInput = moveInputVector;

        //Jump Data
        networkInputData.isJumpButtonPressed = isJumpButtonPressed;

        //Interact data Button "F"
        networkInputData.isFInteractButtonPressed = isFInteractButtonPressed;

        //Interact Data "E"
        networkInputData.isEInteractButtonPressed = isEInteractButtonPressed;
        //Reset variables now that we have read their states
        isJumpButtonPressed = false;
        isFInteractButtonPressed = false;
        isEInteractButtonPressed = false;
        

        return networkInputData;
    }
}
