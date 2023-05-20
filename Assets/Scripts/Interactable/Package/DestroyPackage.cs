using UnityEngine;

public class DestroyPackage : MonoBehaviour
{
    CheckingTriggerWithPlayer checkDeliver;
    DeliveryBehaviour deliveryManager;
    // Start is called before the first frame update
    void Start()
    {
        deliveryManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && checkDeliver.isPackageReady)
        {
            Invoke("DestroyPackages", 2);
        }
    }
    public void DestroyPackages()
    {
        checkDeliver.isPackageReady = false;
        Destroy(this.gameObject);
        Debug.Log("package gone");
        if (this.gameObject.tag == "SmallPackage")
        {
            deliveryManager.currentNumberOfSmallPackages++;
        }
        if (this.gameObject.tag == "Package")
        {
            deliveryManager.currentNumberOfMediumPackages++;
        }
        if (this.gameObject.tag == "BigPackage")
        {
            deliveryManager.currentNumberOfBigPackages++;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeliveyPlatform"))
        {
            checkDeliver = other.GetComponent<CheckingTriggerWithPlayer>();
        }
        
    }
}
