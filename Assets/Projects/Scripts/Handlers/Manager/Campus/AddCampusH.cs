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

    #endregion

    #region -- Methods --

    private string TransferData(CampusD campus)
    {
        return MainHandler.ToJson<CampusD>(campus);
    }

    #endregion

}
