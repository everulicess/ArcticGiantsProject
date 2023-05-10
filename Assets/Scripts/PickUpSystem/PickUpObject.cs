using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    Camera mainCamera;
    bool isCarrying;
    GameObject carriedObject;
    float distance = 2f;
    float smooth = 5f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCarrying)
        {
            Carry(carriedObject);
            CheckDrop();
        }
        else
        {
            PickUp();
        }
    }

    void Carry(GameObject o)
    {
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);

    }
    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if (p != null)
                {
                    isCarrying = true;
                    carriedObject = p.gameObject;
                    p.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

        }
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        isCarrying = false;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }
}
