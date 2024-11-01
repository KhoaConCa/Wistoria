using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface ICreatePaymentHandler 
{
    IEnumerator Upload(PaymentD payment, Action<PaymentD> onSuccess, Action<PaymentD> onError);
}

public interface IPaymentView
{
    void SetPaymentData(PaymentD payment);
}

public interface ISpawnPaymentView
{

}