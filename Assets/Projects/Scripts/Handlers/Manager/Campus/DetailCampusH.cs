using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class DetailCampusH : MonoBehaviour, IDetailCampusUpdateHandler
{
    #region -- Implements --
    public IEnumerator UpdateCampusData(CampusD campus, Action<CampusD> onSuccess, Action<CampusD> onFailed)
    {
        string url = $"{AllUrl.updateCampus}/{campus._id}";
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

    public IEnumerator GetUniqueName(Action<List<string>> onNameCampus)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrl.getCampusUniqueNames))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case
                    UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("Error: " + request.error);
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    onNameCampus?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    onNameCampus?.Invoke(null);
                    break;

                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;
                    List<string> campusNames = MainHandler.FromJson<string>(jsonResponse);
                    onNameCampus.Invoke(campusNames);
                    break;
            }
        }
    }

    public IEnumerator GetUniqueRoom(Action<List<string>> onRoomCampus)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrl.getCampusUniqueRooms))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case
                    UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("Error: " + request.error);
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    onRoomCampus?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    onRoomCampus?.Invoke(null);
                    break;

                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;
                    List<string> campusRoom = MainHandler.FromJson<string>(jsonResponse);
                    onRoomCampus.Invoke(campusRoom);
                    break;
            }
        }
    }

    #endregion

    #region -- Methods --

    public string TransferData(CampusD campus)
    {
        return MainHandler.ToJson<CampusD>(campus);
    }

    #endregion
}
