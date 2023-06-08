using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    public Transform playerModel;



    public override void Spawned()
    {
        bool isReadyScene = SceneManager.GetActiveScene().name == "Ready";

        if (Object.HasInputAuthority)
        {
            Local = this;

            if (isReadyScene)
            {
                //localCameraHandler = new LocalCameraHandler();
            }
            //Set the layer to the local players model
            Utils.SetRenderLayerInChildren(playerModel, LayerMask.NameToLayer("LocalPlayerModel"));

            //Disable main camera
            Camera.main.gameObject.SetActive(false);

            Debug.Log("Spawned local player");
        }
        else 
        {
            //Disable the camera if we are not the local`player
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            //Only 1 audio listener is allowed in the scene so disable remote players audio listener
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;

            Debug.Log("Spawned remote player");

        }

        //Makes it easy to differenciate players
        transform.name = $"P_{Object.Id}";
    }
    
    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);

    }
}
