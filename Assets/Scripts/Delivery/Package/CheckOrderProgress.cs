using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrderProgress : MonoBehaviour
{
    DeliveryBehaviour deliveryManager;
    CheckingTriggerWithPlayer checkDeliver;

    // Start is called before the first frame update
    void Start()
    {
        deliveryManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryBehaviour>();
        checkDeliver = GameObject.Find("Delivery Platform").GetComponent<CheckingTriggerWithPlayer>();
    }

    void OnTriggerEnter(Collider other)
    {
        CheckSmallPackageIn(other);
        CheckMediumPackageIn(other);
        CheckBigPackageIn(other);;
    }
    private void OnTriggerExit(Collider other)
    {
        //CheckSmallPackageOut(other);
        //CheckMediumPackageOut(other);
        //CheckBigPackageOut(other);
    }

    void CheckSmallPackageIn(Collider smallPackage)
    {
        if (smallPackage.CompareTag("SmallPackage"))
        {
            if (deliveryManager.currentNumberOfSmallPackages < deliveryManager.numberSmallPackages && deliveryManager.numberSmallPackages > 0)
            {
                //deliveryManager.currentNumberOfSmallPackages++;
                checkDeliver.isPackageReady = true;
                Debug.Log("checkDeliverIsPackageReady SMALLL");
            }
        }
    }
    void CheckSmallPackageOut(Collider smallPackage)
    {
        if (smallPackage.CompareTag("SmallPackage"))
        {
            deliveryManager.currentNumberOfSmallPackages--;
        }
    }
    void CheckMediumPackageIn(Collider smallPackage)
    {
        if (smallPackage.CompareTag("Package"))
        {
            if (deliveryManager.currentNumberOfMediumPackages < deliveryManager.numberMediumPackages && deliveryManager.numberMediumPackages > 0)
            {
               // deliveryManager.currentNumberOfMediumPackages++;
                checkDeliver.isPackageReady = true;
                Debug.Log("checkDeliverIsPackageReady MEDIUUMMM");
            }
        }
    }
    void CheckMediumPackageOut(Collider smallPackage)
    {
        if (smallPackage.CompareTag("Package"))
        {
            deliveryManager.currentNumberOfMediumPackages--;
        }
    }
    void CheckBigPackageIn(Collider smallPackage)
    {
        if (smallPackage.CompareTag("BigPackage"))
        {
            if (deliveryManager.currentNumberOfBigPackages < deliveryManager.numberBigPackages && deliveryManager.numberBigPackages > 0)
            {
                //deliveryManager.currentNumberOfBigPackages++;
                checkDeliver.isPackageReady = true;
                Debug.Log("checkDeliverIsPackageReady BIIIGGGGG");
            }
        }
    }
    void CheckBigPackageOut(Collider smallPackage)
    {
        if (smallPackage.CompareTag("BigPackage"))
        {
            deliveryManager.currentNumberOfBigPackages--;
        }
    }
}
