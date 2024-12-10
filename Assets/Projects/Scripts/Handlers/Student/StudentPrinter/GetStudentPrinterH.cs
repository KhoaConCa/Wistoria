using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

#region -- Class Description --
/// <summary>
/// Handler class responsible for retrieving package data from a server.
/// Provides methods to fetch all packages or specific packages by paper type.
/// </summary>
#endregion
public class GetStudentPrinterH : MonoBehaviour, IGetStudentPrinterHandler
{
    #region -- Implements --

    /// <summary>
    /// Retrieves all packages from the server.
    /// Executes the provided callback for each package found or with null if an error occurs.
    /// </summary>
    /// <param name="onPackageFound">Callback to execute for each package found.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    public IEnumerator GetAllQueue(Action<QueueD> onQueueFound, Action<string> onSuccess, Action<string> onFailed)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlStudent.getAllQueue))
        {
            yield return request.SendWebRequest();

            MainData<QueueD> response = TransferObjectToData<QueueD>(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                SendData(onQueueFound, response.Data);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Transfer Json data to List data
    /// </summary>
    /// <param name="response">Json string</param>
    public MainData<T> TransferObjectToData<T>(string response)
    {
        MainData<T> mainData = JsonConvert.DeserializeObject<MainData<T>>(response);
        mainData.Initialize();
        return mainData;
    }

    public void SendData(Action<QueueD> onQueueFound, List<QueueD> queueData)
    {
        queueData.Sort((x, y) => y.SlotRemaining.CompareTo(x.SlotRemaining));
        foreach (var itemData in queueData)
        {
            onQueueFound?.Invoke(itemData);
        }
    }
    #endregion
}
