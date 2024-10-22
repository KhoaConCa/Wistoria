using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetCampusH : MonoBehaviour, IFindable
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
                    TransferData(jsonResponse);
                    break;
            }
        }
    }

    #endregion

    #region -- Methods --

    public void TransferData(string response)
    {
        CampusD[] campusArray = BaseHandler.FromJson<CampusD>(response);

        if (campusArray.Length > 0 && campusArray != null)
        {
            CampusD foundCampus = campusArray[0];
            campusDictionary[foundCampus._id] = foundCampus;

            _onCampusFound?.Invoke(foundCampus);
        }
        else
        {
            Debug.Log("No campus found.");
            _onCampusFound?.Invoke(null);
        }
    }

    #endregion

    #region -- Fields --

    private readonly string _getURL = "https://server-wistoria-api.vercel.app/campus/search";

    public Dictionary<string, CampusD> campusDictionary = new Dictionary<string, CampusD>();

    private Action<CampusD> _onCampusFound;

    #endregion
}
