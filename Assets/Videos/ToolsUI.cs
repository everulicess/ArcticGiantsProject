using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolsUI : MonoBehaviour
{
    [SerializeField] TMP_Text wiresText;
    [SerializeField] TMP_Text screwdriverText;
    GameBehaviour gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        
    }

    // Update is called once per frame
    void Update()
    {
        wiresText.text = $"wires {gameManager.wires}";
        screwdriverText.text = $"screwdriver {gameManager.screwdriver}";
    }
}
