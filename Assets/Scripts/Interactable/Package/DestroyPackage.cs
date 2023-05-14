using UnityEngine;

public class DestroyPackage : MonoBehaviour
{
    CheckingTriggerWithPlayer checkDeliver;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && checkDeliver.isPackageReady)
        {
            Invoke("DestroyPackages", 5);
        }
    }
    public void DestroyPackages()
    {
        checkDeliver.isPackageReady = false;
        Destroy(this.gameObject);
        Debug.Log("package gone");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Delivery Platform")
        {
            checkDeliver = other.GetComponent<CheckingTriggerWithPlayer>();

           
        }
        
    }
}
