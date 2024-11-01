using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class CreatePaymentC : MonoBehaviour
{
    #region -- Implements --
    
    public void ClickPackage()
    {
        ClickPPaymentMethod();
        gameObject.SetActive(false); // Hide PPurchasePaper button
    }

    public void ClickPPaymentMethod()
    {
        UploadPaymentInformation();

    }

    void Start()
    {
/*        getButton.onClick.AddListener(ClickPackage);
        getButton.onClick.AddListener(ClickPPaymentMethod);*/
    }

    public void UploadPaymentInformation()
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

    #endregion

    #region -- Fields --

    public Button getButton;

    private ICreatePaymentHandler _createPayment;

    #endregion 
}
