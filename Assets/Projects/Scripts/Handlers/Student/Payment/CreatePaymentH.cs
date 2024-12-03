using System;
using System.Text;
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
    #endregion

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
