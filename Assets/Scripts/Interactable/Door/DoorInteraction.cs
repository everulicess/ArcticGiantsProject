using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
         Interacting();
        
    }
    void Interacting()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y,1));


            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var objectHit = hit.collider.gameObject.GetComponent<DoorOpening>();

                if (hit.collider.CompareTag("Door"))
                {
                    objectHit.isDoorOpened = !objectHit.isDoorOpened;

                    Debug.Log($"DOOR BUTTON HIT {objectHit.isDoorOpened}");
                    //if (objectHit.isDoorOpened == true)
                    //{
                    //    objectHit.isDoorOpened = false;
                    //}
                    //else
                    //{
                    //    objectHit.isDoorOpened = true;
                    //}
                    

                }
            }
        }
    }
}
