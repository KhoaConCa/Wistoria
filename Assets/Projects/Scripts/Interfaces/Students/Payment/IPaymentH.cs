using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatePaymentHandler
{
    /// <summary>
    /// Coroutine for uploading payment data to the server.
    /// Executes a success callback if upload is successful or an error callback if it fails.
    /// </summary>
    /// <param name="payment">The payment data to be uploaded.</param>
    /// <param name="onSuccess">Callback executed when upload is successful.</param>
    /// <param name="onError">Callback executed when upload fails.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    IEnumerator Upload(PaymentD payment, Action<PaymentD> onSuccess, Action<PaymentD> onError);
}

public interface IPaymentProcessor
{
    void ProcessPayment(PackageD package, string studentId);
}

