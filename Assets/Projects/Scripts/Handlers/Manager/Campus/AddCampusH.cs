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

    public IEnumerator AddNewCampus(CampusD campus, Action<CampusD> onSuccess)
    {
        string json = TransferData(campus);

        using (UnityWebRequest www = UnityWebRequest.Post(AllUrl.createCampus, json, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Campus upload completed!");
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

    #endregion

    #region -- Methods --

    private string TransferData(CampusD campus)
    {
        return MainHandler.ToJson<CampusD>(campus);
    }

    #endregion

}
