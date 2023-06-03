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
    //Rescue timer
    [SerializeField]
    Timer timeUntilRes;
    //Resources
    [SerializeField]
    Slider oxygenBar;
    [SerializeField]
    Slider energyBar;

    //Lights
    public int numberOfLightsOn = 5;

    //Tools for repairing
    public int wrench=0;
    public int screwdriver=0;
    public int pliers=0;
    public int wires=0;

    //Id for Activating Signals
    public bool hasID = false;

    //Control over the player
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

    //GameStates
    GameState gameState = GameState.PlayingVideo;

    enum GameState
    {
        PlayingVideo,
        RepairingStuff,
        WaitingForRescue,
        Dead,
        Rescued


    }
    private void Awake()
    {
        //timeUntilRes = GameObject.Find("Timer").GetComponent<Timer>();
        //oxygenBar = GameObject.Find("OxygenBar").GetComponent<OxygenBar>();
        //energyBar = GameObject.Find("ElectricityBar").GetComponent<EnergyBar>();
    }
    private void Update()
    {
        
        switch (gameState)
        {
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

    //Playing introduction video
    //videoPlayer
    public GameObject vp;
    void PlayingVideo(GameObject vp)
    {
        isPlayerControl = false;
        vp.SetActive(true);
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
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //Rescued
    void Rescued()
    {

        Debug.Log("YOU HAVE BEEN RESCUED FROM THE GAME MANAGER");
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    //Check For Oxygen
    void OxygenCheck()
    {
        if (isOxygenFixed)
        {
            oxygenBar.GetComponent<OxygenBar>().currentOxygen += 15 * Time.deltaTime;
            if (energyBar.GetComponent<EnergyBar>().currentEnergy <= 0)
            {
                if (oxygenBar.GetComponent<OxygenBar>().currentOxygen <= 0)
                {
                    gameState = GameState.Dead;
                }
            }
        }
        else
        {
            oxygenBar.GetComponent<OxygenBar>().currentOxygen -= 15 * Time.deltaTime;
            energyBar.GetComponent<EnergyBar>().currentEnergy -= 5 * Time.deltaTime;
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
