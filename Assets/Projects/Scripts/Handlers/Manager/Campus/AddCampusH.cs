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
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrl.getCampusUniqueNames))
        {
            yield return request.SendWebRequest();

            MainData<string> response = TransferStringToData(request.downloadHandler.text);

            switch (request.result)
            {
                case
                    UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("Error: " + request.error);
                    onFaild?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    onFaild?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    onFaild?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.Success:
                    onSuccess?.Invoke(response.Message);

                    onNameCampus.Invoke(response.Data);
                    break;
            }
        }
    }

    public IEnumerator AddNewCampus(CampusD campus, Action<string> onSuccess, Action<string> onFaild)
    {
        string json = TransferDataToJson(campus);

        using (UnityWebRequest request = UnityWebRequest.Post(AllUrl.createCampus, json, "application/json"))
        {
            yield return request.SendWebRequest();

            MainData<CampusD> newCampus = TransferObjectToData(request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)
                onFaild?.Invoke(newCampus.Message);
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
