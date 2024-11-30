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

    public IEnumerator CreateMOMOPayment(MomoD momo, Action<MomoD> onSuccess, Action<string> onFaild)
    {
        string json = TransferDataToJson(momo);
        Debug.Log(json);

        using (UnityWebRequest request = UnityWebRequest.Post(AllUrlStudent.createMOMO, json, "application/json"))
        {
            yield return request.SendWebRequest();

            MainData<MomoD> newMomo = TransferObjectToData(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)

                onFaild?.Invoke(newMomo.Message);
            else
                onSuccess?.Invoke(newMomo.Data[0]);
        }
    }

    public IEnumerator GetCallback(string orderId, Action<MomoD> onMOMOFound, Action<string> onSuccess, Action<string> onFaild)
    {
        string searchURL = $"{AllUrlStudent.getMOMO}/{orderId}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            MainData<MomoD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                if (response.Data.Count == 1)
                {
                    MomoD momo = response.Data[0];
                    onMOMOFound?.Invoke(momo);
                }
            }
            else
                onFaild?.Invoke(response.Message);
        }
    }

    public IEnumerator DeleteCallback(string orderId, Action<string> onSuccess, Action<string> onFaild)
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
                onFaild?.Invoke(errorMessage);
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
}
