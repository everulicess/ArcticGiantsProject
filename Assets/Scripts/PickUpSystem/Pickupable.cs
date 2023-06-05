using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField]
    GameObject anchorPoint;

    bool isConnected = false;
    public bool isBeingPickedUp = true;
    WiresConnected wiresConnected;
    private void Start()
    {
        wiresConnected = GameObject.Find("Circuit box").GetComponent<WiresConnected>();
    }

    private void Update()
    {
        if (!isBeingPickedUp)
        {
            if (isConnected == false)
            {
                Debug.Log("IsNOTconnected");
                float x = anchorPoint.transform.position.x + 0.3f;
                float y = anchorPoint.transform.position.y;
                float z = anchorPoint.transform.position.z;
                this.gameObject.transform.position = new Vector3(x, y, z);

            }
            else
            {
                Debug.Log("Isconnected");
            }
        }
        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Conector")
        {
            float x = other.gameObject.transform.position.x;
            float y = other.gameObject.transform.position.y;
            float z = other.gameObject.transform.position.z;
            this.gameObject.transform.position = new Vector3(x, y, z);
            isConnected = true;
            wiresConnected.ConnectedWires += 1;
            //Debug.Log("Connector");
            Destroy(this.gameObject.GetComponent<Pickupable>());
        }
    }
}
