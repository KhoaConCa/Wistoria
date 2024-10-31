using System;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class CreatePaymentH : MonoBehaviour, ICreatePaymentHandler
{
    #region -- Fields --

    private readonly string _createURL = "https://server-wistoria-api.vercel.app/payment/create";

    #endregion

    #region -- Public Methods --

    /// <summary>
    /// Uploads a payment request to the server
    /// </summary>
    /// <param name="payment">Payment data to upload</param>
    /// <param name="onSuccess">Callback when request is successful</param>
    /// <param name="onError">Callback when request fails</param>
    public IEnumerator Upload(PaymentD payment, Action<PaymentD> onSuccess, Action<PaymentD> onError)
    {
        string jsonPayload = JsonConvert.SerializeObject(payment);
        UnityWebRequest request = CreatePostRequest(_createURL, jsonPayload);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            HandleSuccess(request, onSuccess);
        }
        else
        {
            HandleError(request, onError);
        }
    }

    #endregion

    #region -- Private Methods --

    /// <summary>
    /// Creates a POST request with JSON payload
    /// </summary>
    private UnityWebRequest CreatePostRequest(string url, string jsonPayload)
    {
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonPayload);
        UnityWebRequest request = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(jsonBytes),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    /// <summary>
    /// Handles a successful server response.
    /// </summary>
    private void HandleSuccess(UnityWebRequest request, Action<PaymentD> onSuccess)
    {
        PaymentD responsePayment = JsonConvert.DeserializeObject<PaymentD>(request.downloadHandler.text);
        onSuccess?.Invoke(responsePayment);
    }

    /// <summary>
    /// Handles an error response from the server.
    /// </summary>
    private void HandleError(UnityWebRequest request, Action<PaymentD> onError)
    {
        Debug.LogError($"Request Failed: {request.error}");
        onError?.Invoke(null);
    }

    #endregion
}
