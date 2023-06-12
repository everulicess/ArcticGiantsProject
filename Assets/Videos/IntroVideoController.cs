using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Video;
using Fusion;

public class IntroVideoController : NetworkBehaviour
{

    GameBehaviour gameManager;

    [SerializeField]
    VideoPlayer videoPlayer;
    [SerializeField]
    GameObject canvasUI;

    
    private void Awake()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    //Debug.Log("UI disabled");
    //    //canvasUI.SetActive(false);
    //}
    private void OnEnable()
    {
        PlayIntroVideo();
    }

    public override void FixedUpdateNetwork()
    {
        //Debug.Log($"this is the frame: {videoPlayer.frame} and this is the FRAME COUNT {videoPlayer.frameCount}");
        if (videoPlayer.isPlaying)
        {
            if (!gameManager.isVideoFinished)
            {
                CheckIfVideoIsPlaying();
            }
        }


    }

    void CheckIfVideoIsPlaying()
    {
        //Debug.Log("Checking is the video  is playing");
        if (videoPlayer.isPlaying)
        {
            //player.GetComponent<CharacterController>().enabled = false;
            //camera.GetComponent<LocalCameraHandler>().enabled = false;
            canvasUI.SetActive(false);

            gameManager.isVideoPlaying = true;

        }
        else
        {
            //player.GetComponent<CharacterController>().enabled = true;
            //camera.GetComponent<LocalCameraHandler>().enabled = true;
            gameManager.isVideoFinished = true;
            canvasUI.SetActive(true);
            this.gameObject.SetActive(false);
            
            
        }
    }
    public void PlayIntroVideo()
    {
        videoPlayer.Play();
        
        
    }
    
}
