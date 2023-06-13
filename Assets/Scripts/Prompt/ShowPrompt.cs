using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPrompt : MonoBehaviour
{
    public TMP_Text interactText;
    public string stringText;
    bool isShowingPrompt = false;
    bool isPlayerNear = false;
    //forpickUp
    public bool isCarrying;
    private void Start()
    {
        interactText.enabled = true;
    }
    private void Update()
    {
        interactText.text = stringText;
        if (isPlayerNear)
        {
            if (!isShowingPrompt)
            {
                ShowThePrompt();
            }
            ShowThePrompt();
        }
        else if (!isPlayerNear )
        {
            if (isCarrying)
            {
                ShowThePrompt();
            }
            else if (!isCarrying)
            {
                HideThePrompt();
            }
            HideThePrompt();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShowingPrompt)
        {
            isPlayerNear = true;
            //ShowThePrompt();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            //HideThePrompt();
        }
    }
    public void ShowThePrompt()
    {
        isShowingPrompt = true;
        interactText.enabled = true;
    }
    public void HideThePrompt()
    {
        isShowingPrompt = false;
        interactText.enabled = false;
    }
}
