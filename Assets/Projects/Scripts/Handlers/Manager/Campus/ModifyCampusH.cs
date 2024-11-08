using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;

public class ModifyCampusH : MonoBehaviour, IModifyCampusHandler
{
    #region -- Implements --

    /// <summary>
    /// Transfer Json data to List data
    /// </summary>
    /// <param name="response">Json string</param>
    public void TransferData(string response)
    {
        List<CampusD> campusList = MainHandler.FromJson<CampusD>(response);

        if (campusList != null && campusList.Count > 0)
        {
            Debug.Log(campusList.Count);
            foreach (var campus in campusList)
            {
                _onCampusFound?.Invoke(campus);
            }
        }
        else
        {
            Debug.Log("No campus found.");
            _onCampusFound?.Invoke(null);
        }
    }

    public IEnumerator CampusInformation(CampusD campus, Action<CampusD> onCampusFound)
    {
        _onCampusFound = onCampusFound;

        string searchURL = $"{_getURL}?name={UnityWebRequest.EscapeURL(campus.CampusName)}" +
            $"&room={UnityWebRequest.EscapeURL(campus.Room)}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    _onCampusFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    onCampusFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;
                    Debug.Log(jsonResponse);
                    TransferData(jsonResponse);
                    break;
            }
        }
    }

    #endregion

    #region -- Methods --

    public IEnumerator UpdateCampusData(CampusD campus, Action<CampusD> onCampusUpdated)
    {
        string updateURL = $"{_modifyURL}{campus._id}";
        string jsonData = JsonUtility.ToJson(campus);

        using (UnityWebRequest request = new UnityWebRequest(updateURL, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    onCampusUpdated?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    onCampusUpdated?.Invoke(null);
                    break;

                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;
                    CampusD updatedCampus = JsonUtility.FromJson<CampusD>(jsonResponse);
                    onCampusUpdated?.Invoke(updatedCampus);
                    break;
            }
        }
    }

    #endregion

    #region -- Fields --

    private readonly string _modifyURL = "";
    private readonly string _getURL = "https://server-wistoria-api.vercel.app/campus/search/name";

    private Action<CampusD> _onCampusFound;

    #endregion
}