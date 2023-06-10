using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInteraction : MonoBehaviour
{

    private PlayerInventory playerInventory;
    private void Start()
    {
        // Get the reference to the PlayerInventory component
        playerInventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        // Detect player interaction with items 
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast or trigger-based detection to determine if the player is near an item
            // If the player is near an item, call the Collect() method on the item GameObject
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
            {
                Item item = hit.collider.GetComponent<Item>();
                if (item != null)
                {
                    item.Collect();
                }
            }
        }
    }
}
