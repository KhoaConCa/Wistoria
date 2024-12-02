using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetStoreStudentH : MonoBehaviour, IGetStoreHandler
{
    #region -- Implements --

    /// <summary>
    /// GET all store data form server
    /// </summary>
    /// <param name="onStoreFound">Method will be call when store information is found</param>
    /// <returns></returns>
    public IEnumerator GetAllStore(Action<StoreD> onStoreFound, Action<string> onSuccess, Action<string> onFaild)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlStudent.getAllStore))
        {
            yield return request.SendWebRequest();

            MainData<StoreD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                SendData(onStoreFound, response);

                onSuccess?.Invoke(response.Message);
            }
            else
                onFaild?.Invoke(response.Message);
        }
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Transfer Json data to List data
    /// </summary>
    /// <param name="response">Json string</param>
    public MainData<StoreD> TransferObjectToData(string response)
    {
        MainData<StoreD> mainData = JsonConvert.DeserializeObject<MainData<StoreD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<string> TransferStringToData(string response)
    {
        MainData<string> mainData = JsonConvert.DeserializeObject<MainData<string>>(response);
        mainData.Initialize();
        return mainData;
    }

    public void SendData(Action<StoreD> onStoreFound, MainData<StoreD> storeDatas)
    {
        foreach (var itemData in storeDatas.Data)
        {
            onStoreFound?.Invoke(itemData);
        }
    }

    #endregion
}
