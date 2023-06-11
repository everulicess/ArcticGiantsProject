using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Fusion;
using UnityEditor;

public class GameBehaviour : MonoBehaviour
{
    //This comment is for pushing to see if lolo can have what i have from the GitHub
    //Rescue timer
    [SerializeField]
    Timer timeUntilRes;
    //Resources
    [SerializeField]
    Slider oxygenBar;

    [SerializeField]
    Slider energyBar;

    //Lights
    public int numberOfLightsOn = 9;

    //Tools for repairing
    public int wrench=0;
    public int screwdriver=0;
    public int pliers=0;
    public int wires=0;

    //Id for Activating Signals
    public bool hasID = false;

    //Control over the player
    public bool isPlayerControl = false;
    public int playersNumber;

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

    //GameStates
    GameState gameState = GameState.WaitingForPlayerLoaded;

    enum GameState
    {
        WaitingForPlayerLoaded,
        PlayingVideo,
        RepairingStuff,
        WaitingForRescue,
        Dead,
        Rescued


    }
    //private void Awake()
    //{
        //timeUntilRes = GameObject.Find("Timer").GetComponent<Timer>();
        //oxygenBar = GameObject.Find("OxygenBar").GetComponent<OxygenBar>();
        //energyBar = GameObject.Find("ElectricityBar").GetComponent<EnergyBar>();
    //}
    private void Update()
    {
        Debug.Log($"THIS IS THE NUMBER OF PLAYERS IN THE SESSION {playersNumber}");   
        switch (gameState)
        {
            case GameState.WaitingForPlayerLoaded:
                PlayersLoaded();
                ;break;
            case GameState.PlayingVideo:
                PlayingVideo(vp);
                break;
            case GameState.RepairingStuff:
                RepairingStuff();
                ; break;
            case GameState.WaitingForRescue:
                WaitingForRescue();
                break;
            case GameState.Dead:
                Dead();
                ; break;
            case GameState.Rescued:
                Rescued();
                ; break;
            default:
                break;
        }
    }
    //GameMethods
    //WaitingForPlayerLoaded
    public void PlayersLoaded()
    {
        if (playersNumber == 2)
        {
            gameState = GameState.PlayingVideo;
        }
        
    }

    //Playing introduction video
    //videoPlayer
    public GameObject vp;
    void PlayingVideo(GameObject vp)
    {
        isPlayerControl = false;
        vp.SetActive(true);
        vp.GetComponent<IntroVideoController>().PlayIntroVideo();
        if (isVideoFinished)
        {
            isPlayerControl = true;
            vp.SetActive(false);
            Debug.Log("VIDEO FINISHED");
            gameState = GameState.RepairingStuff;
        }
    }

    //Repairing phase
   
    void RepairingStuff()
    {
        
        OxygenCheck();
        if (isTableFixed)
        {
            Debug.Log("Change State waiting for rescue");
            gameState = GameState.WaitingForRescue;
        }

    }

    //Waiting For Rescue
    void WaitingForRescue()
    {
        Debug.Log("RES RES RES RES RES");
        if (timeUntilRes.time <= 0)
        {
            gameState = GameState.Rescued;
        }
        timeUntilRes.startTimer = true;
        OxygenCheck();

    }

    //Dead
    void Dead()
    {
        Debug.Log("NICE TRY, GOOD LUCK NEXT TIME");
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //Rescued
    void Rescued()
    {

        Debug.Log("YOU HAVE BEEN RESCUED FROM THE GAME MANAGER");
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //Check For Oxygen
    void OxygenCheck()
    {
        if (isOxygenFixed)
        {
            //If is fixed and have energy
                //oxygen increases 1
                //consumes energy 0.5
              //if there is no energy
                //oxygen decrease 1
            oxygenBar.GetComponent<OxygenBar>().decreaseRate = -1f;
            energyBar.GetComponent<EnergyBar>().decreaseRate += 0.5f;
            if (energyBar.GetComponent<EnergyBar>().currentEnergy <= 0)
            {
                oxygenBar.GetComponent<OxygenBar>().decreaseRate = 1f;
                if (oxygenBar.GetComponent<OxygenBar>().currentOxygen <= 0)
                {
                    gameState = GameState.Dead;
                }
            }
        }
        else
        {
            //if is not fixed
            //oxygen decreases 1
            oxygenBar.GetComponent<OxygenBar>().decreaseRate = 1f;
            if (oxygenBar.GetComponent<OxygenBar>().currentOxygen <= 0)
            {
                gameState = GameState.Dead;
            }
        }
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
