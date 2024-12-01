using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetManagerH : MonoBehaviour, IGetManagerH
{
    #region -- Implements --

    public IEnumerator GetManagerByID(string id, Action<ManagerD> onSuccess, Action<string> onFailed)
    {

        //set URL for system to search the item
        string json = AllUrlManager.findManagerByID + id;

        using (UnityWebRequest request = UnityWebRequest.Get(json))
        {
            yield return request.SendWebRequest();

            MainData<ManagerD> response = TransferStringToManagerD(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Data[0]);
            else
                onFailed?.Invoke(response.Message);
        }
    }

    #endregion

    #region -- Methods --

    public MainData<ManagerD> TransferStringToManagerD(string response)
    {
        MainData<ManagerD> mainData = JsonConvert.DeserializeObject<MainData<ManagerD>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion
}
