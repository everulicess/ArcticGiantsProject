using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    Camera mainCamera;
    bool isCarrying;
    GameObject carriedObject;
    float distance = 1.45f;
    float smooth = 5f;

    GameBehaviour GameManager;
    ShowPrompt showPrompt;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehaviour>();
        mainCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    public void Update()
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
        float x = 23.97f;
        float y = this.gameObject.transform.position.y;
        float z = -5.04f;
        this.gameObject.transform.position = new (x, y, z);// (o.transform.position.z+2));
        GameManager.isPlayerControl = false;
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);

    }

    public void PickUp()
    {
        if (Input.GetMouseButtonDown(0))//(Input.GetKeyDown(KeyCode.E))
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
                    //p.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
                else
                {
                    isCarrying = false; 
                    carriedObject = null;
                }
            }

        }
    }

    void CheckDrop()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.isPlayerControl = true;
            DropObject();
        }
    }

    void DropObject()
    {
        //showPrompt.stringText = "Pick Up [E]";
        //showPrompt.isCarrying = false;
        isCarrying = false;
        //carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Package")|| other.CompareTag("SmallPackage")|| other.CompareTag("BigPackage"))
        {
            ComparePackageTag(other);
        }
    }

    void ComparePackageTag(Collider other)
    {
        showPrompt = other.GetComponent<ShowPrompt>();
        showPrompt.stringText = "Pick Up [E]";

        if (isCarrying)
        {
            showPrompt.isCarrying = true;
            showPrompt.stringText = "Drop[E]";
            Debug.Log("change to drop E");
        }
        else
        {

            showPrompt.stringText = "Pick Up [E]";
            Debug.Log("change to Pick Up E");
        }

    }
}
