using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class DetailCampusH : MonoBehaviour, IDetailCampusUpdateHandler
{
    #region -- Implements --

    public IEnumerator GetUniqueName(Action<List<string>> onNameCampus, Action<string> onSuccess, Action<string> onFailed)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getCampusUniqueNames))
        {
            yield return request.SendWebRequest();

            MainData<string> response = TransferStringToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                onNameCampus?.Invoke(response.Data);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator UpdateCampusData(CampusD campus, Action<string> onSuccess, Action<string> onFailed)
    {
        string url = $"{AllUrlManager.updateCampus}/{campus.Id}";

        string json = TransferDataToJson(campus);
        Debug.Log(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<CampusD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Message);
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator GetUniqueRoom(Action<List<string>> onRoomCampus, Action<string> onSuccess, Action<string> onFailed)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getCampusUniqueRooms))
        {
            yield return request.SendWebRequest();

            MainData<string> response = TransferStringToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                onRoomCampus?.Invoke(response.Data);
            }
            else
                onFailed?.Invoke(response.Message);
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
