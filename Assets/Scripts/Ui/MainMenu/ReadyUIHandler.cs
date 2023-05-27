using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Fusion;

public class NewBehaviourScript : MonoBehaviour
{
    //SessionInfo sessionInfo;

    //public event Action<SessionInfo> OnJoinSession;

    public void SetInformation(SessionInfo sessionInfo)
    {
        //this.sessionInfo = sessionInfo;
        Debug.Log($"Name Of The Session {sessionInfo.Name}, Player Count {sessionInfo.PlayerCount}/{sessionInfo.MaxPlayers}------------------------------");
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("-----------------------------------------------------------------------------------");
    }
}
