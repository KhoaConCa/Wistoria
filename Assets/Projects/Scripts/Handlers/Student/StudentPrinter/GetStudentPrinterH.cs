using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;
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
    public IEnumerator GetAllStudentPrinter(Action<StudentPrinterD> onStudentPrinterFound, Action<string> onSuccess, Action<string> onFaild)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlStudent.getStudentPrinter))
        {
            yield return request.SendWebRequest();

            MainData<StudentPrinterD> response = TransferObjectToData(request.downloadHandler.text);
            Debug.Log(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                SendData(onStudentPrinterFound, response);
            }
            else
                onFaild?.Invoke(response.Message);
        }
    }
    #endregion

    #region -- Methods --

    /// <summary>
    /// Transfer Json data to List data
    /// </summary>
    /// <param name="response">Json string</param>
    public MainData<StudentPrinterD> TransferObjectToData(string response)
    {
        MainData<StudentPrinterD> mainData = JsonConvert.DeserializeObject<MainData<StudentPrinterD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<string> TransferStringToData(string response)
    {
        MainData<string> mainData = JsonConvert.DeserializeObject<MainData<string>>(response);
        mainData.Initialize();
        return mainData;
    }

    public void SendData(Action<StudentPrinterD> onStudentPrinterFound, MainData<StudentPrinterD> studentPrinterDatas)
    {
        Debug.LogWarning(studentPrinterDatas.Data.Count);
        foreach (var itemData in studentPrinterDatas.Data)
        {
            onStudentPrinterFound?.Invoke(itemData);
        }
    }
    #endregion
}
