using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBehaviour : MonoBehaviour
{
    public int timeBetweenOrders = 4;

    public bool isOrderCompleted = true;

    //order number of packages
    public int numberSmallPackages;
    public int numberMediumPackages;
    public int numberBigPackages;

    //current number of packages delivered
    public int currentNumberOfSmallPackages;
    public int currentNumberOfMediumPackages;
    public int currentNumberOfBigPackages;

    //range for packages
    int minPackageRange = 1;
    int maxPackageRange = 3;

    private void Start()
    {
        GenerateOrder();
    }

    private void FixedUpdate()
    {
        CheckForEndedOrder();
    }

    void CheckForEndedOrder()
    {
        if (currentNumberOfSmallPackages == numberSmallPackages && currentNumberOfMediumPackages == numberMediumPackages && currentNumberOfBigPackages == numberBigPackages )
        {
            isOrderCompleted = true;
            Invoke("GenerateOrder", timeBetweenOrders);
        }
        else
        {
            isOrderCompleted = false;
        }
    }

    void GenerateOrder()
    {
        ResetCurrentNumberOfPackages();

        numberSmallPackages = GenerateSmallPackageNumber();
        numberMediumPackages = GenerateMediumPackageNumber();
        numberBigPackages = GenerateBigPackageNumber();

        //Debug.Log($"small{numberSmallPackages} medium {numberMediumPackages} big {numberBigPackages}");
        return;

    }
    public int GenerateSmallPackageNumber()
    {
        int smallPackageNumber = Random.Range(minPackageRange, maxPackageRange);
        return smallPackageNumber;
    }
    public int GenerateMediumPackageNumber()
    {
        int mediumPackageNumber = Random.Range(minPackageRange, maxPackageRange);
        return mediumPackageNumber;
    }
    public int GenerateBigPackageNumber()
    {
        isOrderCompleted = false;
        int bigPackageNumber = Random.Range(minPackageRange, maxPackageRange);
        return bigPackageNumber;
    }
    void ResetCurrentNumberOfPackages()
    {
        currentNumberOfSmallPackages = 0;
        currentNumberOfMediumPackages = 0;
        currentNumberOfBigPackages = 0;
    }
}
