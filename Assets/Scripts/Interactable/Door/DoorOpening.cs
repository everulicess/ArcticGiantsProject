using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField]
    Animator doorAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorAnim.SetBool("openDoor",true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetBool("openDoor", true);
        }
    }private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetBool("openDoor", false);
        }
    }
}
