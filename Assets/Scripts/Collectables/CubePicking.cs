using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class CubePicking : NetworkBehaviour
{
    bool isPlayerNear;
    GameBehaviour gameManager;
    [SerializeField]
    string itemName;
    [SerializeField]
    GameObject exclamationMark;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
    }

    //enum Item
    //{
    //    wires,
    //    wrench,
    //    screwdriver

    //}
    public override void FixedUpdateNetwork()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (itemName)
                {
                    case "wires":
                        gameManager.wires += 1;
                        Debug.Log($"Wires {gameManager.wires} INCREASED");
                        break;
                    case "pliers":
                        gameManager.pliers += 1;
                        //Debug.Log($"PLIERS {gameManager.pliers} INCREASED");
                        break;
                    case "screwdriver":
                        gameManager.screwdriver += 1;
                        //Debug.Log($"SCREWDRIVERS {gameManager.screwdriver} INCREASED");
                        break;
                    case "ID":
                        gameManager.hasID = true;
                        //Debug.Log($"ID {gameManager.hasID} INCREASED");
                        break;
                    default:
                        return;
                }

                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
    private void CollectItemRPC(GameObject item)
    {
        ItemIndicator indicator = item.GetComponent<ItemIndicator>();
        if (indicator != null)
        {
            indicator.RemoveExclamationMark();
        }
    }
    }
