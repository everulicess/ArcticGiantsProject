using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemIndicator : MonoBehaviour
{
    public GameObject exclamationMarkPrefab;
    private GameObject exclamationMarkInstance;

    private void Start()
    {
        SpawnExclamationMark();
    }

    private void SpawnExclamationMark()
    {
        exclamationMarkInstance = Instantiate(exclamationMarkPrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
    }

    public void RemoveExclamationMark()
    {
        if (exclamationMarkInstance != null)
        {
            Destroy(exclamationMarkInstance);
        }
    }
}


