using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Video;
using Fusion;

public class IntroVideoController : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlayer;
    [SerializeField]
    GameObject canvasUI;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("UI disabled");
        canvasUI.SetActive(false);
        //PlayIntroVideo();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"this is the frame: {videoPlayer.frame} and this is the FRAME COUNT {videoPlayer.frameCount}");
        Invoke("CheckIfVideoIsPlaying", 2);
        

    }

    void CheckIfVideoIsPlaying()
    {
        var player = GameObject.FindWithTag("Player");
        var camera = GameObject.FindObjectOfType<Camera>();
        //Debug.Log("Checking is the video  is playing");
        if (videoPlayer.isPlaying)
        {
            player.GetComponent<CharacterController>().enabled = false;
            camera.GetComponent<LocalCameraHandler>().enabled = false;
            canvasUI.SetActive(false);

        }
        else
        {
            player.GetComponent<CharacterController>().enabled = true;
            camera.GetComponent<LocalCameraHandler>().enabled = true;
            canvasUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
    public void PlayIntroVideo()
    {
        this.gameObject.SetActive(true);
        videoPlayer.Play();
    }
    
}
