using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Fusion;

public class GameBehaviour : MonoBehaviour
{
    //Lights
    public int numberOfLightsOn = 5;

    public bool isPlayerControl = false;

    //Booleans gameFlow
    [Networked]
    public bool isVideoPlaying { get; set; } = false;
    [Networked]
    public bool isVideoFinished { get; set; } = false;
    [Networked]
    public bool isOxygenFixed { get; set; } = false;
    [Networked]
    public bool isTableFixed { get; set; } = false;
    [Networked]
    public bool isSolarPanels { get; set; } = false;
    [Networked]
    public bool isNextStage { get; set; } = false;

    //GameStates
    GameState gameState = GameState.playingVideo;

    enum GameState
    {
        playingVideo,
        finishedVideo,
        oxygenNeedsFixing,
        tableNeedsFixing,
        solarPanelsFixing,
        waitingForRescue


    }
    private void Update()
    {
        switch (gameState)
        {
            case GameState.playingVideo:
                PlayingVideo(vp);
                break;
            case GameState.finishedVideo:
                FinishedVideo(vp);
                break;
            case GameState.oxygenNeedsFixing:
                OxygenNeedsFixing();
                break;
            case GameState.solarPanelsFixing:
                SolarPanelsNeedFixing();
                break;
            case GameState.tableNeedsFixing:
                TableNeedsFixing();
                break;
            case GameState.waitingForRescue:
                WaitingForRescue();
                break;
            default:
                break;
        }
    }
    //GameMethods


    //StartTheGame
    //Playerprefab
    public GameObject player;
    /*void StartingGame()
    {
        player.SetActive(false);
        Debug.Log("Starting game state");
        Invoke("",3);
        if (isVideoPlaying)
        {
            gameState = GameState.playingVideo;
            Debug.Log("changing from start to video play");
        }
    }*/


    //Playing introduction video
    //videoPlayer
    public GameObject vp;
    void PlayingVideo(GameObject vp)
    {
        isPlayerControl = false;
        vp.SetActive(true);
        if (isVideoFinished)
        {
            gameState = GameState.finishedVideo;
            Debug.Log("play video ----------------Finish video");
        }
    }

    //Finished Video
    void FinishedVideo(GameObject vp)
    {
        isPlayerControl = true;
        vp.SetActive(false);
        Debug.Log("VIDEO FINISHED");
        if (!isOxygenFixed)
        {
            gameState = GameState.oxygenNeedsFixing;
        }
    }

    //Oxygen Needs Fixing
    void OxygenNeedsFixing()
    {
        //Decrease Oxygen drastically

        
        if (isOxygenFixed)
        {
            //Decrease energy always on another script that manages oxygen
            gameState = GameState.tableNeedsFixing;
        }
    }

    //Solar Panels Fixing
    void SolarPanelsNeedFixing()
    {

    }

    //Table Needs Fixing
    void TableNeedsFixing()
    {
        if (isTableFixed)
        {

        }

    }

    //Waiting For Rescue
    void WaitingForRescue()
    {

    }
    //Lights
    public int lights
    {
        get { return numberOfLightsOn; }

        set
        {
            numberOfLightsOn = value;
            Debug.LogFormat("lights: {0}", numberOfLightsOn);
        }
    }
}
