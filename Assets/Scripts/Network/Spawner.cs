using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPlayer playerPrefab;
    //other component
    CharacterInputHandler characterInputHandler;

    GameBehaviour gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        spawningPlayer = SpawningPlayer.FirstPlayer;
    }
    SpawningPlayer spawningPlayer;

    enum SpawningPlayer
    {
        FirstPlayer,
        SecondPlayer

    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {

        if (runner.IsServer)
        {
            Debug.Log("OnPlayerJoined we are server. Spawning player");
            
            switch (spawningPlayer)
            {
                case SpawningPlayer.FirstPlayer:
                    runner.Spawn(playerPrefab, Utils.GetFirstSpawnPoint("First"), Quaternion.identity, player);
                    spawningPlayer = SpawningPlayer.SecondPlayer;
                    break;
                case SpawningPlayer.SecondPlayer:
                    runner.Spawn(playerPrefab, Utils.GetFirstSpawnPoint("Second"), Quaternion.identity, player);
                    break;
                default:
                    runner.Spawn(playerPrefab, Utils.GetFirstSpawnPoint("First"), Quaternion.identity, player);
                    break;
            }

        }
        else
        {
            Debug.Log("OnPlayerJoined");
        }
    }


    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (characterInputHandler == null && NetworkPlayer.Local !=null)
        {
            characterInputHandler = NetworkPlayer.Local.GetComponent<CharacterInputHandler>();
        }

        if (characterInputHandler != null)
        {
            input.Set(characterInputHandler.GetNetworkInputData());
        }
    }





    public void OnConnectedToServer()
    {
        
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {}
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("OnShutDown");
    }
    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("OnDisconnectFromServer");
    }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("OnConnectRequest");
    }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectFailed");
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {}
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {}
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {}
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {}
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {}
    public void OnSceneLoadDone(NetworkRunner runner)
    {}
    public void OnSceneLoadStart(NetworkRunner runner)
    {}


    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log($"OnConnectedToServer");
    }
}
