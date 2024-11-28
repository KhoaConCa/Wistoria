using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class DetailStoreH : MonoBehaviour, IDetailStoreUpdateHandler
{
    #region -- Implements --

    public IEnumerator UpdateStoreData(StoreD store, Action<string> onSuccess, Action<string> onFailed)
    {
        string url = $"{AllUrlManager.updateStore}/{store.Id}";

        string json = TransferDataToJson(store);
        Debug.Log(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<StoreD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Message);
            else
                onFailed?.Invoke(response.Message);
        }
    }

    #endregion

    #region -- Methods --

    public string TransferDataToJson(StoreD store)
    {
        return MainHandler.ToJson<StoreD>(store);
    }

    public MainData<StoreD> TransferObjectToData(string response)
    {
        MainData<StoreD> mainData = JsonConvert.DeserializeObject<MainData<StoreD>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion
}
