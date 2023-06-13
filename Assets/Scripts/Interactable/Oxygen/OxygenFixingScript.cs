using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class OxygenFixingScript : MonoBehaviour
{

    GameBehaviour GameManager;
    //Particles system
    [SerializeField]
    ParticleSystem[] oxygenParticles;
    [SerializeField]
    GameObject repairIcon;

    [SerializeField]
    float iconHeight = 1.0f;

    //player near
    bool isPlayerNear = false;

    [Networked]
    public bool oxygenNeedsFixing { get; set; } = false;
    [Networked]
    public bool oxygenIsFixed { get; set; } = false;

    OxygenState oxygenState;
    enum OxygenState
    {
        NeedsFixing,
        FixedAndWorking,
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        oxygenState = OxygenState.NeedsFixing;

        if (repairIcon != null)
        {
            repairIcon.transform.position = transform.position + new Vector3(0, iconHeight, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (oxygenState)
        {
            case OxygenState.NeedsFixing:
                NeedsFixing();
                break;
            case OxygenState.FixedAndWorking:
                FixedAndWorking();
                break;
            default:
                break;
        }
    }


    //NeedsFixing
    void NeedsFixing()
    {
        foreach (var particleSystem in oxygenParticles)
        {
            particleSystem.Play();
        }
        
        //show what is needed to fix the oxygen into the UI
        if (isPlayerNear)
        {
            int wiresNum = 1; /*int wrenchNum = 0;*/ int pliersNum = 1; int screwdriverNum = 1;

            if (GameManager.wires >= wiresNum && GameManager.pliers >= pliersNum && GameManager.screwdriver >= screwdriverNum)
            {
                Debug.Log("Fixing oxygen");
                oxygenState = OxygenState.FixedAndWorking;
                foreach (var particleSystem in oxygenParticles)
                {
                    particleSystem.Stop();
                }

                if (repairIcon != null)
                {
                    repairIcon.SetActive(false);
                }
            }
        }
        
    }

    //Fixed and Working
    void FixedAndWorking()
    {
        GameManager.isOxygenFixed = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log($"{isPlayerNear} oxygen");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log($"{isPlayerNear} no oxygen");
        }
    }
}
