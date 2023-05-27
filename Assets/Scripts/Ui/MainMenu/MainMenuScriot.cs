using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScriot : MonoBehaviour
{
    //public Canvas videoCanvas;
    //public VideoPlayer introductionVideo;
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Modeled option A");
    }
}
