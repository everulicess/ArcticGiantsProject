using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfItems { get; private set; }

    public UnityEvent<int> OnItemCollected;

    public void CollectItem()
    {
        NumberOfItems++;
        OnItemCollected.Invoke(NumberOfItems);
    }
}