using System;
using System.Collections;

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
    IEnumerator Upload(PaymentD payment, Action<string> onSuccess, Action<string> onFaild);
    IEnumerator UploadJson(PaymentJsonD payment, Action<string> onSuccess, Action<string> onFailed);
    IEnumerator UpdateAfterPayment(PaymentJsonD payment, Action<string> onSuccess, Action<string> onFailed);
}

public interface IPaymentProcessor
{
    IEnumerator UploadPaymentToMongoDB(PaymentD payment, Action<string> onSuccess, Action<string> onFaild);
}

