using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface ICreatePaymentHandler 
{
    IEnumerator Upload(PaymentD payment, Action<string> onSuccess, Action<string> onError);
}

public interface IPaymentView
{
    void SetPaymentData(PaymentD payment);
}