using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePicking : MonoBehaviour
{
    bool isPlayerNear;
    GameBehaviour gameManager;
    [SerializeField]
    string itemName;

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
    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (itemName)
                {
                    case "wires":
                        gameManager.wires += 1;
                        break;
                    case "wrench":
                        gameManager.wrench += 1;
                        break;
                    case "screwdriver":
                        gameManager.screwdriver += 1;
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

    private void CollectItem(GameObject item)
    {
        ItemIndicator indicator = item.GetComponent<ItemIndicator>();
        if (indicator != null)
        {
            indicator.RemoveExclamationMark();
        }
    }
    }
