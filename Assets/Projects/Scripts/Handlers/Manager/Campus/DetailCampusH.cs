using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class DetailCampusH : MonoBehaviour, IDetailCampusUpdateHandler
{
    #region -- Implements --

    public string TransferData(CampusD campus)
    {
        return MainHandler.ToJson<CampusD>(campus);
    }

    public IEnumerator UpdateCampusData(CampusD campus, Action<CampusD> onSuccess, Action<CampusD> onFailed)
    {
        string url = $"{_updateURL}/{campus._id}";
        Debug.Log(url);

        string json = TransferData(campus);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                CampusD updatedCampus = JsonUtility.FromJson<CampusD>(request.downloadHandler.text);
                onSuccess?.Invoke(campus);
            }
            else
            {
                Debug.LogError("Error updating campus: " + request.error);
                onFailed?.Invoke(campus);
            }
        }
    }

    #endregion

    #region -- Methods --

    #endregion

    #region -- Fields --

    private readonly string _updateURL = "https://server-wistoria-api.vercel.app/campus/update";

    #endregion
}
