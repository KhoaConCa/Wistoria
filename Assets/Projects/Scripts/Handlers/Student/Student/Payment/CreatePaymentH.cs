using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;
using System.Text;

public class CreatePaymentH : MonoBehaviour, ICreatePaymentHandler
{
    #region -- Implements --

    // Start is called before the first frame update

    public IEnumerator Upload(PaymentD payment, Action<string> onSuccess, Action<string> onError)
    {
        // chuyen du lieu
        string json = ParseJson.ToJson<PaymentD>(payment);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
/*        using (UnityWebRequest request = new UnityWebRequest(_createURL, "POST"))
*/
        Debug.Log(json);
        Debug.Log("jsonBytes as string: " + Encoding.UTF8.GetString(jsonBytes));


        UnityWebRequest request = new UnityWebRequest(_createURL, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Gửi request và đợi phản hồi
        yield return request.SendWebRequest();

        // Xử lý kết quả từ server
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Response: {request.downloadHandler.text}");
            onSuccess?.Invoke(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
            onError?.Invoke(request.error);
        }
    }

    #endregion

    #region -- Fields --

    private readonly string _createURL = "https://server-wistoria-api.vercel.app/payment/create";

    #endregion

}