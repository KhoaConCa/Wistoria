using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class ModifyCampusH : MonoBehaviour, IModifyCampusHandler
{
    #region -- Implements --

    public IEnumerator CampusInformation(CampusD campus, Action<CampusD> onCampusFound)
    {
        _onCampusFound = onCampusFound;

        using (UnityWebRequest request = UnityWebRequest.Get(_modifyURL))
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
                    break;
            }
        }
    }

    #endregion

    #region -- Methods --


    #endregion

    #region -- Fields --

    private readonly string _modifyURL = "";

    private Action<CampusD> _onCampusFound;

    #endregion
}
