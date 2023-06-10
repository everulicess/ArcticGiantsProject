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
    SessionInfo thisSesionInfo;
    private void Awake()
    {
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();

        if (networkRunnerInScene != null)
            networkRunner = networkRunnerInScene;
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
                var clientTask = InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, "TestSession",NetAddress.Any(),SceneManager.GetActiveScene().buildIndex, null);
            }

        }
        

        Debug.Log($"Server NetworkRunner Started.");
    }

    private void FixedUpdate()
    {
        //if (SceneManager.GetActiveScene().name == "Ready")
        //{
        //    CheckIfSessionIsFull();
        //}
    }

    void CheckIfSessionIsFull(SessionInfo thisSessionInfo)
    {
        //Debug.Log($"PLAYER COUNT IS {thisSesionInfo.PlayerCount} MAX PLAYERS ARE {thisSesionInfo.MaxPlayers}");
        if (thisSesionInfo.PlayerCount == thisSesionInfo.MaxPlayers)
        {
            SceneManager.LoadScene("New modeled");
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
            CustomLobbyName = "OurLobbyID",
            PlayerCount = 2,
            SessionName = sessionName,
            Initialized = initialized,
            SceneManager = sceneManager
        });
    }
    public void OnJoinLobby()
    {
        var clientTask = JoinLobby();
    }

    private async Task JoinLobby()
    {

        Debug.Log("JoinLobby started");

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
        Debug.Log($"Create session {sessionName} scene {sceneName} build Index {SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}")}");

        //Join existing game as a client
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Host, sessionName, NetAddress.Any(), SceneUtility.GetBuildIndexByScenePath($"scenes/{sceneName}"), null);

    }

    public void JoinGame(SessionInfo sessionInfo)
    {
        Debug.Log($"Join session {sessionInfo.Name}");
        //thisSesionInfo = sessionInfo;
        //Join existing game as a client
        var clientTask = InitializeNetworkRunner(networkRunner, GameMode.Client, sessionInfo.Name, NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);
    }
}
