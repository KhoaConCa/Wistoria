using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Utilities;

public class PaymentProcessor : MonoBehaviour , IPaymentProcessor
{
    public IEnumerator UploadPaymentToMongoDB(PaymentD payment)
    {
        if (payment == null)
        {
            Debug.LogError("Payment data is null. Cannot proceed with upload.");
            yield break;
        }

        // Convert payment data to JSON
        string json = MainHandler.ToJson(payment, true);
        Debug.Log("JSON being sent: " + json);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        // Set up the POST request
        using (UnityWebRequest request = new UnityWebRequest("https://server-wistoria-api.vercel.app/payment/create", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 30;

            // Send the request
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Payment data uploaded successfully.");
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Failed to upload payment data: " + request.error);
                Debug.LogError("Response Code: " + request.responseCode);
                Debug.LogError("Response: " + request.downloadHandler.text);
            }
        }
    }
}
