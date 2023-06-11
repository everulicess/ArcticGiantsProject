using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ChangeButtonColor : MonoBehaviour
{

    [SerializeField]
    Material[] buttonColors;
    Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = buttonColors[0];
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void ChangeColorRPC(string buttonState)
    {
        switch (buttonState)
        {
            case "Opened":
                rend.sharedMaterial = buttonColors[0];
                ;break;
            case "Closed":
                rend.sharedMaterial = buttonColors[1];
                ;break;
            default:
                break;
        }
    }
}
