using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class GetCampusH : MonoBehaviour, IGetCampusHandler
{
    #region -- Implements --

    public IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound)
    {
        _onCampusFound = onCampusFound;

        // Using Query parameter ?name= to set campusName parameter
        string searchURL = $"{_getURL}?name={UnityWebRequest.EscapeURL(campusName)}";

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
                    _onCampusFound?.Invoke(null);
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

    public void TransferData(string response)
    {
        List<CampusD> campusList = ParseJson.FromJson<CampusD>(response);

        if (campusList != null && campusList.Count > 0)
        {
            Debug.Log(campusList.Count);
            foreach (var campus in campusList)
            {
                Debug.Log($"Campus Name: {campus.CampusName}, Room: {campus.Room}");
                _onCampusFound?.Invoke(campus);
            }
        }
        else
        {
            Debug.Log("No campus found.");
            _onCampusFound?.Invoke(null);
        }
    }

    #endregion

    #region -- Fields --

    private readonly string _getURL = "https://server-wistoria-api.vercel.app/campus/search/name";

    private Action<CampusD> _onCampusFound;

    #endregion
}