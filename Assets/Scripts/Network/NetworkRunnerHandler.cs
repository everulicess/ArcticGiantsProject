using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;


public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;

    NetworkRunner networkRunner;

    private void Awake()
    {
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();

        //If we have a network runner then we shouldn't create a new one when changing scenes
        if (networkRunnerInScene != null)
        {
            networkRunner = networkRunnerInScene;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (networkRunner == null)
        {
            networkRunner = Instantiate(networkRunnerPrefab);
            networkRunner.name = "Network Runner";

            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, "TestSession", NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
                
            }
            Debug.Log($"Server NetworkRunner Started.");
        }
    }

    protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode,string sessionName, NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
    {
        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();
        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = address,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = "OurLobbyID",
            PlayerCount = 1,
            Initialized = initialized,
            SceneManager = sceneManager
        });;
    }

    public void OnJoinLobby()
    {
        var clientTask = JoinLobby();
    }
    private async Task JoinLobby()
    {
        Debug.Log("JoinLobby Started");
        string lobbyID = "OurLobbyID";

        var result = await networkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyID);
        if (!result.Ok)
        {
            Debug.LogError($"Unable to join lobby {lobbyID}");
        }
        else
        {
            Debug.Log("JoinLobby ok");
        }

    }

    public void CreateGame(string sessionName, string sceneName)
    {
        Debug.Log($"Create Game {sessionName} scene {sceneName} build index {SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}")}");

        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Host, sessionName, NetAddress.Any(), SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}"), null);
    }
    public void JoinGame(SessionInfo sessionInfo)
    {
        Debug.Log($"Join session {sessionInfo.Name}");

        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Client, sessionInfo.Name, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
    }
}
