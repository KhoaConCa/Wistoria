using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface ICreatePaymentHandler 
{

}

public interface IPaymentView
{
    void SetPaymentData(PaymentD payment);
}