using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManagerCampusH : MonoBehaviour
{
    #region -- Methods --

    public IEnumerator GetCampus(string campusName, Action<CampusD> onCampusFound)
    {
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
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    break;

                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;

                    CampusD[] campusArray = MainHandler.FromJson<CampusD>(jsonResponse);

                    if (campusArray.Length > 0)
                    {
                        CampusD foundCampus = campusArray[0];
                        campusDictionary[foundCampus._id] = foundCampus;

                        Debug.Log($"Campus added: {foundCampus.CampusName}");
                        onCampusFound?.Invoke(foundCampus);
                    }
                    else
                    {
                        Debug.Log("No campus found.");
                    }
                    break;
            }
        }
    }

    #endregion

    #region -- Fields --

    private string _getURL = "https://server-wistoria-api.vercel.app/campus/search";
    private string _postURL = "https://server-wistoria-api.vercel.app/campus/create";

    public Dictionary<string, CampusD> campusDictionary = new Dictionary<string, CampusD>();

    #endregion
}
