using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class AddCampusH : MonoBehaviour, IAddCampusHandler
{
    #region -- Implements --

    public IEnumerator GetUniqueName(Action<List<string>> onNameCampus, Action<string> onSuccess, Action<string> onFaild)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getCampusUniqueNames))
        {
            yield return request.SendWebRequest();

            MainData<string> response = TransferStringToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                onNameCampus.Invoke(response.Data);
            }
            else
                onFaild?.Invoke(response.Message);
        }
    }

    public IEnumerator AddNewCampus(CampusD campus, Action<string> onSuccess, Action<string> onFailed)
    {
        string json = TransferDataToJson(campus);

        using (UnityWebRequest request = UnityWebRequest.Post(AllUrlManager.createCampus, json, "application/json"))
        {
            yield return request.SendWebRequest();

            MainData<CampusD> newCampus = TransferObjectToData(request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)
                onFailed?.Invoke(newCampus.Message);
            else
                onSuccess?.Invoke(newCampus.Message);
        }
    }

    #endregion

    #region -- Methods --

    public string TransferDataToJson(CampusD campus)
    {
        return MainHandler.ToJson<CampusD>(campus);
    }

    public MainData<CampusD> TransferObjectToData(string response)
    {
        MainData<CampusD> mainData = JsonConvert.DeserializeObject<MainData<CampusD>>(response);
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
