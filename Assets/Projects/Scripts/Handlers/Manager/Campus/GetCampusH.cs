using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;
using Newtonsoft.Json;

public class GetCampusH : MonoBehaviour, IGetCampusHandler
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

    /// <summary>
    /// GET all campus data form server
    /// </summary>
    /// <param name="onCampusFound">Method will be call when campus information is found</param>
    /// <returns></returns>
    public IEnumerator GetAllCampus(Action<CampusD> onCampusFound, Action<string> onSuccess, Action<string> onFaild)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrl.getAllCampus))
        {
            yield return request.SendWebRequest();

            MainData<CampusD> response = TransferObjectToData(request.downloadHandler.text);

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    onFaild?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    onFaild?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.Success:
                    onSuccess?.Invoke(response.Message);

                    SendData(onCampusFound, response);
                    break;
            }
        }
    }

    /// <summary>
    /// GET campus by name form server
    /// </summary>
    /// <param name="campusName">Name campus</param>
    /// <param name="onCampusFound">Method will be call when campus information is found</param>
    /// <returns></returns>
    public IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound, Action<string> onSuccess, Action<string> onFaild)
    {
        string searchURL = $"{AllUrl.searchCampusByName}?name={UnityWebRequest.EscapeURL(campusName)}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            MainData<CampusD> response = TransferObjectToData(request.downloadHandler.text);

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:

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

                    SendData(onCampusFound, response);
                    break;
            }
        }
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Transfer Json data to List data
    /// </summary>
    /// <param name="response">Json string</param>
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

    public void SendData(Action<CampusD> onCampusFound, MainData<CampusD> campusDatas)
    {
        foreach (var itemData in campusDatas.Data)
        {
            onCampusFound?.Invoke(itemData);
        }
    }

    #endregion
}