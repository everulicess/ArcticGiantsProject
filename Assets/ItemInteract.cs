using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour

{
    // Define a reference to the player's interaction button (e.g., "E" key)
    public KeyCode interactionKey = KeyCode.E;

    public object GameManager { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            TryCollectItem();
        }
    }

    private void TryCollectItem()
    {
        // Perform a raycast from the player's position forward to detect nearby items
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Check if the hit object has an "Item" component
            Item item = hit.collider.GetComponent<Item>();
            if (item != null)
            {
                CollectItem(item);
            }
        }
    }

    private void CollectItem(Item item)
    {
        // Perform the necessary actions when an item is collected
        // For example, increase the player's score, play a sound effect, etc.
        int scoreToAdd = item.scoreValue;
        

        // Destroy the collected item
        Destroy(item.gameObject);
    }
}

