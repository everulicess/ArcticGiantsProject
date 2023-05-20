using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeliveryUI : MonoBehaviour
{
    //Notification
    public GameObject notification;
    public GameObject notificationNew;

    //Manager
    DeliveryBehaviour deliveryManager;

    //TextVariables
    public TMP_Text newOrderText;
    public TMP_Text orderState;

    bool isShowingOrder = false;

    private void Start()
    {
        notification.SetActive(true);
        notificationNew.SetActive(false);

        deliveryManager = GameObject.Find("DeliveryManager").GetComponent<DeliveryBehaviour>();
    }

    private void Update()
    {
        isShowingOrder = deliveryManager.isOrderCompleted;
        if (isShowingOrder)
        {
            notification.SetActive(false);
            notificationNew.SetActive(true);
        }
        else
        {
            notification.SetActive(true);
            notificationNew.SetActive(false);
        }

        //Text for the UI
        orderState.text = $"{deliveryManager.currentNumberOfSmallPackages}/{deliveryManager.numberSmallPackages} Small," +
            $"{deliveryManager.currentNumberOfMediumPackages}/{deliveryManager.numberMediumPackages} Medium," +
            $"{deliveryManager.currentNumberOfBigPackages}/{deliveryManager.numberBigPackages} Big";
        newOrderText.text = $" New Order in {deliveryManager.timeBetweenOrders}";
    }
}
