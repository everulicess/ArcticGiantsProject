using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrderProgress : MonoBehaviour
{
    DeliveryBehaviour deliveryManager;
    CheckingTriggerWithPlayer checkDelivery;

    private void Start()
    {
        deliveryManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryBehaviour>();
        checkDelivery = GetComponent<CheckingTriggerWithPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckSmallPackageIn(other);
        CheckMediumPackageIn(other);
        CheckBigPackageIn(other);
    } 
    void CheckSmallPackageIn(Collider package)
    {
        if (package.CompareTag("SmallPackage"))
        {
            if (deliveryManager.currentNumberOfSmallPackages < deliveryManager.numberSmallPackages)
            {
                checkDelivery.isPackageReady = true;
            }
        }
    }
    void CheckMediumPackageIn(Collider package)
    {
        if (package.CompareTag("Package"))
        {
            if (deliveryManager.currentNumberOfMediumPackages < deliveryManager.numberMediumPackages)
            {
                checkDelivery.isPackageReady = true;
            }
        }
    }
    void CheckBigPackageIn(Collider package)
    {
        if (package.CompareTag("BigPackage"))
        {
            if (deliveryManager.currentNumberOfBigPackages < deliveryManager.numberBigPackages)
            {
                checkDelivery.isPackageReady = true;
            }
        }
    }
}
