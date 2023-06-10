using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    public Transform playerModel;

    public LocalCameraHandler localCameraHandler;


    public override void Spawned()
    {

        if (Object.HasInputAuthority)
        {
            Local = this;

           
                //Sets the layer of the local players model
                Utils.SetRenderLayerInChildren(playerModel, LayerMask.NameToLayer("LocalPlayerModel"));

                ////Disable main camera
                if (Camera.main != null)
                    Camera.main.gameObject.SetActive(false);

                //Enable the local camera
                localCameraHandler.localCamera.enabled = true;
                localCameraHandler.gameObject.SetActive(true);


                ////Detach camera if enabled
                localCameraHandler.transform.parent = null;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            
            Debug.Log("Spawned local player");
        }
        else 
        {
            
            //Disable the local camera for remote players
            localCameraHandler.localCamera.enabled = false;
            localCameraHandler.gameObject.SetActive(false);

            Debug.Log($"{Time.time} Spawned remote player");
        }

        //Set the Player as a player object
        //Runner.SetPlayerObject(Object.InputAuthority, Object);

        //Makes it easy to differenciate players
        transform.name = $"P_{Object.Id}";
    }
    
    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);

    }

    //void OnDestroy()
    //{
    //    //Get rid of the local camera if we get destroyed as a new one will be spawned with the new Network player
    //    if (localCameraHandler != null)
    //        Destroy(localCameraHandler.gameObject);
    //}

    //void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log($"{Time.time} OnSceneLoaded: " + scene.name);

    //    if (scene.name != "Ready")
    //    {
    //        //Tell the host that we need to perform the spawned code manually. 
    //        if (Object.HasStateAuthority && Object.HasInputAuthority)
    //            Spawned();

    //        if (Object.HasStateAuthority)
    //            GetComponent<CharacterMovementHandler>().RequestRespawn();
    //    }
    //}

}
