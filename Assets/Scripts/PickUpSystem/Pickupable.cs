using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField]
    GameObject anchorPoint;

    WiresConnected wiresConnected;
    private void Start()
    {
        wiresConnected = GameObject.Find("Circuit box").GetComponent<WiresConnected>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Conector")
        {
            wiresConnected.wiresConnected += 1;
            //Debug.Log("Connector");
            Destroy(this.gameObject.GetComponent<Pickupable>());
        }
        else
        {
            float x = anchorPoint.transform.position.x + 0.3f;
            float y = anchorPoint.transform.position.y;
            float z = anchorPoint.transform.position.z;
            this.gameObject.transform.position = new Vector3(x,y,z);
        }
    }
}
