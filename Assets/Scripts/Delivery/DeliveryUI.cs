using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using TMPro;

public class DeliveryUI : MonoBehaviour
{
    //notification
    public GameObject notification;

    DeliveryBehaviour deliveryManager;
    public TMP_Text newOrderText;

    public TMP_Text orderState;
    public bool isShowingOrder = false;

    public 
    // Start is called before the first frame update
    void Start()
    {
        notification.SetActive(false);
        deliveryManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryBehaviour>();
       
    }

    // Update is called once per frame
    void Update()
    {
        isShowingOrder = deliveryManager.isOrderCompleted;
        if (isShowingOrder)
        {
           
            notification.SetActive(false);
        }
        else
        {
            notification.SetActive(true);
        }
        orderState.text = $"{deliveryManager.currentNumberOfSmallPackages}/{deliveryManager.numberSmallPackages} small," +
              $" {deliveryManager.currentNumberOfMediumPackages}/{deliveryManager.numberMediumPackages} medium," +
              $" {deliveryManager.currentNumberOfBigPackages}/{deliveryManager.numberBigPackages}";
        newOrderText.text = $"New Order {deliveryManager.timeBetweenOrders} big";


    }
    private void FixedUpdate()
    {
       
    }
}
