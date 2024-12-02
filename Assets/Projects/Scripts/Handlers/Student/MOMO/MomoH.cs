using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class MomoH : MonoBehaviour, IMomoHandler
{
    #region -- Implements --

    public IEnumerator CreateMOMOPayment(MomoD momo, Action<MomoD> onSuccess, Action<string> onFailed)
    {
        string json = TransferDataToJson(momo);
        Debug.Log(json);

        using (UnityWebRequest request = UnityWebRequest.Post(AllUrlStudent.createMOMO, json, "application/json"))
        {
            yield return request.SendWebRequest();

            MainData<MomoD> newMomo = TransferObjectToData(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)

                onFailed?.Invoke(newMomo.Message);
            else
                onSuccess?.Invoke(newMomo.Data[0]);
        }
    }

    public IEnumerator GetCallback(string orderId, Action<int> onSuccess, Action<string> onFailed)
    {
        string searchURL = $"{AllUrlStudent.getMOMO}/{orderId}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            MainData<MomoD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Data[0].ResultCode);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator DeleteCallback(string orderId, Action<string> onSuccess, Action<string> onFailed)
    {
        string deleteURL = $"{AllUrlStudent.deleteCallback}/{orderId}";

        using (UnityWebRequest request = UnityWebRequest.Delete(deleteURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                MainData<MomoD> response = TransferObjectToData(request.downloadHandler.text);
                onSuccess?.Invoke(response.Message);
            }
            else
            {
                string errorMessage = request.downloadHandler?.text ?? "Failed to delete Momo payment";
                onFailed?.Invoke(errorMessage);
            }
        }
    }
    #endregion

    #region -- Methods --

    public string TransferDataToJson(MomoD momo)
    {
        return MainHandler.ToJson<MomoD>(momo);
    }

    public MainData<MomoD> TransferObjectToData(string response)
    {
        MainData<MomoD> mainData = JsonConvert.DeserializeObject<MainData<MomoD>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion

    #region -- Fields --
    #endregion
}
