using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region -- Interface for Payment Upload Handler --
/// <summary>
/// Interface for handling payment upload operations.
/// Defines methods to upload payment data and execute callbacks on success or error.
/// </summary>
#endregion
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

#region -- Interface for Payment View --
/// <summary>
/// Interface for displaying payment data in a view.
/// Defines methods to set payment data in the view.
/// </summary>
#endregion
public interface IPaymentView
{
    /// <summary>
    /// Sets payment data in the view for display.
    /// </summary>
    /// <param name="payment">The payment data to display.</param>
    void SetPaymentData(PaymentD payment);
}
