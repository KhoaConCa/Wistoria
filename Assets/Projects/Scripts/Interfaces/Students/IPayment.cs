using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IPaymentCommand
{
}

public interface IGetPaymentCommand 
{

}


public interface IGetPaymentHandler 
{

}

public interface IPaymentView
{
    void SetPaymentData(PaymentD payment);
}