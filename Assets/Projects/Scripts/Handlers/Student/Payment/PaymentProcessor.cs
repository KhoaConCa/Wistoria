using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using Newtonsoft.Json;
using System;

public class PaymentProcessor : MonoBehaviour, IPaymentProcessor
{
    public IEnumerator UploadPaymentToMongoDB(PaymentD payment, Action<string> onSuccess, Action<string> onFaild)
    {
        if (payment == null)
        {
            Debug.LogError("Payment data is null. Cannot proceed with upload.");
            yield break;
        }

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

    #region -- Methods --

    public string TransferDataToJson(PaymentD campus)
    {
        return MainHandler.ToJson<PaymentD>(campus);
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

    #endregion
}
