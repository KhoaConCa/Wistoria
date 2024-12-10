using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class AddStoreH : MonoBehaviour, IAddStoreHandler
{
    #region -- Implements --

    public IEnumerator AddNewStore(StoreD store, Action<string> onSuccess, Action<string> onFaild)
    {
        string json = TransferDataToJson(store);

        using (UnityWebRequest request = UnityWebRequest.Post(AllUrlManager.createStore, json, "application/json"))
        {
            yield return request.SendWebRequest();

            MainData<StoreD> newStore = TransferObjectToData(request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)
                onFaild?.Invoke(newStore.Message);
            else
                onSuccess?.Invoke(newStore.Message);
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
