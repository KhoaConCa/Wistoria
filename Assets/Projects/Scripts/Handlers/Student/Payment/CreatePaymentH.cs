using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class CreatePaymentH : MonoBehaviour, ICreatePaymentHandler
{
    #region -- Implements --

    /// <summary>
    /// Uploads a payment request to the server
    /// </summary>
    /// <param name="payment">Payment data to upload</param>
    /// <param name="onSuccess">Callback when request is successful</param>
    /// <param name="onError">Callback when request fails</param>
    public IEnumerator Upload(PaymentD payment, Action<string> onSuccess, Action<string> onFaild)
    {
        string json = TransferDataToJson(payment);

        using (UnityWebRequest request = UnityWebRequest.Post(AllUrlStudent.createPayment, json, "application/json"))
        {
            yield return request.SendWebRequest();

            MainData<PaymentD> newCampus = TransferObjectToData(request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)
                onFaild?.Invoke(newCampus.Message);
            else
                onSuccess?.Invoke(newCampus.Message);
        }
    }

    public IEnumerator UploadJson(PaymentJsonD payment, Action<string> onSuccess, Action<string> onFailed)
    {
        string json = TransferToJson(payment);
        Debug.Log(json);

        using (UnityWebRequest request = UnityWebRequest.Post(AllUrlStudent.createPayment, json, "application/json"))
        {
            yield return request.SendWebRequest();

            Debug.LogWarning(request.downloadHandler.text);
            MainData<PaymentJsonD> newPayment = TransferToData(request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)
                onFailed?.Invoke(newPayment.Message);
            else
                onSuccess?.Invoke(newPayment.Message);
        }
    }
    #endregion

    #region -- Methods --

    public string TransferDataToJson(PaymentD payment)
    {
        return MainHandler.ToJson<PaymentD>(payment);
    }

    public MainData<PaymentD> TransferObjectToData(string response)
    {
        MainData<PaymentD> mainData = JsonConvert.DeserializeObject<MainData<PaymentD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<string> TransferStringToData(string response)
    {
        MainData<string> mainData = JsonConvert.DeserializeObject<MainData<string>>(response);
        mainData.Initialize();
        return mainData;
    }
    public string TransferToJson(PaymentJsonD payment)
    {
        return MainHandler.ToJson<PaymentJsonD>(payment);
    }

    public MainData<PaymentJsonD> TransferToData(string response)
    {
        MainData<PaymentJsonD> mainData = JsonConvert.DeserializeObject<MainData<PaymentJsonD>>(response);
        mainData.Initialize();
        mainData.Data[0].ProgressPerson();
        return mainData;
    }


    #endregion
}
