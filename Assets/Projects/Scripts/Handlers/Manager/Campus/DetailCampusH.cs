using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class DetailCampusH : MonoBehaviour, IDetailUpdate
{
    #region -- Implements --

    public IEnumerator UpdateCampusData(CampusD campus, Action<CampusD> onSuccess, Action onFailed)
    {
        _onSuccess = onSuccess;
        _onFailed = onFailed;
        string url = $"{_updateURL}/{campus._id}";

        string json = TransferData(campus);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Gửi yêu cầu và chờ phản hồi
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Phân tích JSON phản hồi và chuyển thành đối tượng CampusD
                CampusD updatedCampus = JsonUtility.FromJson<CampusD>(request.downloadHandler.text);
                _onSuccess?.Invoke(updatedCampus);
            }
            else
            {
                Debug.LogError("Error updating campus: " + request.error);
                _onFailed?.Invoke();
            }
        }
    }



    #endregion

    #region -- Methods --

    private string TransferData(CampusD campus)
    {
        return MainHandler.ToJson<CampusD>(campus); ;
    }

    #endregion

    #region -- Fields --

    private Action<CampusD> _onSuccess;
    private Action _onFailed;

    private readonly string _updateURL = "";

    #endregion
}
