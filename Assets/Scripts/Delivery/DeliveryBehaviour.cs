using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DeliveryBehaviour : NetworkBehaviour
{
    public int timeBetweenOrders = 20;
    public int numberSmallPackages;
    public int numberMediumPackages;
    public int numberBigPackages;
    public int currentNumberOfSmallPackages;
    public int currentNumberOfMediumPackages;
    public int currentNumberOfBigPackages;

    public bool isOrderCompleted;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isOrderCompleted = false;
            GenerateOrder();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isOrderCompleted = true;
            
        }
        //reseting the order time
        
        //Debug.Log($"{timeBetweenOrders} tiempo antes de siguiente orden");


    }
    private void FixedUpdate()
    {
        
    }

    public void GenerateOrder()//int smallPackagesAmount, int mediumPackagesAmount, int bigPackagesAmount)
    {
        numberSmallPackages= GenerateSmallPackagesNumber();
        numberMediumPackages = GenerateMediumPackagesNumber();
        numberBigPackages = GenerateBigPackagesNumber();
        Debug.Log($"small:{numberSmallPackages} Medium:{numberMediumPackages} Big:{numberBigPackages}");
    }

    public int GenerateSmallPackagesNumber()
    {
        int smallPackagesNumber = Random.Range(0, 4);
        return smallPackagesNumber;
    }
    public int GenerateMediumPackagesNumber()
    {
        int mediumPackagesNumber = Random.Range(0,4);
        return mediumPackagesNumber;
    }public int GenerateBigPackagesNumber()
    {
        int bigPackagesNumber = Random.Range(0, 4);
        return bigPackagesNumber;
    }
}
