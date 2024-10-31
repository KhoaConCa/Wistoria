using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class CreatePaymentC : MonoBehaviour
{
    public void ClickPPurchasePaper()
    {
        
    }

    void Start()
    {
        _createPayment = gameObject.AddComponent<CreatePaymentH>();

        PaymentD payment = new PaymentD
        {
            Paper = "50",
            Person = "671860901e0844975517030e",
            Status = "Finished"
        };

        StartCoroutine(_createPayment.Upload(payment, onSuccess: response => Debug.Log($"Success: {response}"), onError: error => Debug.LogError($"Failed: {error}")));
    }

    private ICreatePaymentHandler _createPayment;
}
