using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUPScrewdriver : MonoBehaviour
{
    public GameObject Screwdriver; // Reference to the in-game object where the child will be added
    public GameObject NetworkPlayerPF; // Reference to the prefab containing the child object

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                GameObject instantiatedObject = Instantiate(NetworkPlayerPF, Screwdriver.transform);
                instantiatedObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}


