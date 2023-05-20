using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DeliveryBehaviour : NetworkBehaviour
{
    public int timeBetweenOrders = 4;

    //Order packages number
    public int numberSmallPackages;
    public int numberMediumPackages;
    public int numberBigPackages;

    //current packages delivered
    public int currentNumberOfSmallPackages;
    public int currentNumberOfMediumPackages;
    public int currentNumberOfBigPackages;

    public bool isOrderCompleted = true;

    // Start is called before the first frame update
    void Start()
    {
        GenerateOrder();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEndedOrder();

    }

    void CheckForEndedOrder()
    {
        if (currentNumberOfSmallPackages == numberSmallPackages && currentNumberOfMediumPackages == numberMediumPackages && currentNumberOfBigPackages ==  numberBigPackages)
        {
            Debug.Log("--------------------------------------------------------------------------------------------------------");
            isOrderCompleted = true;
            Invoke("GenerateOrder", timeBetweenOrders);
        }
        else
        {
            
            isOrderCompleted = false;
        }
    }
    void ResetCurrentNumberOfPackages()
    {
        currentNumberOfSmallPackages = 0;
        currentNumberOfMediumPackages = 0;
        currentNumberOfBigPackages = 0;
    }


    void GenerateOrder()
    {
        ResetCurrentNumberOfPackages();

        numberSmallPackages = GenerateSmallPackagesNumber();
        numberMediumPackages = GenerateMediumPackagesNumber();
        numberBigPackages = GenerateBigPackagesNumber();
        Debug.Log($"------------------small:{numberSmallPackages} Medium:{numberMediumPackages} Big:{numberBigPackages}---------------");
        return;
        
    }

    int minPackageRange = 1;
    int maxPackageRange = 3;
    public int GenerateSmallPackagesNumber()
    {
        int smallPackagesNumber = Random.Range(minPackageRange, maxPackageRange);
        return smallPackagesNumber;
    }
    public int GenerateMediumPackagesNumber()
    {
        int mediumPackagesNumber = Random.Range(minPackageRange, maxPackageRange);
        return mediumPackagesNumber;
    }public int GenerateBigPackagesNumber()
    {
        int bigPackagesNumber = Random.Range(minPackageRange, maxPackageRange);
        isOrderCompleted = false;
        return bigPackagesNumber;
    }
}
