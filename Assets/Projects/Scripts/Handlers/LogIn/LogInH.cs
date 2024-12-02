using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LogInH : MonoBehaviour, ILogInHandler
{
    #region -- Implements --

    public IEnumerator SearchStudentById(string id, Action<string> onSuccess, Action<string> onFailed)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlStudent.getStudentByID + id))
        {

            yield return request.SendWebRequest();

            MainData<StudentD> response = TransferStringToStudentD(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Data[0].Id);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public IEnumerator SearchManagerById(string id, Action<string> onSuccess, Action<string> onFailed)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getManagerByID + id))
        {

            yield return request.SendWebRequest();

            MainData<ManagerD> response = TransferStringToManagerD(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Data[0].Id);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    #endregion

    #region -- Methods --

    public MainData<StudentD> TransferStringToStudentD(string response)
    {
        MainData<StudentD> mainData = JsonConvert.DeserializeObject<MainData<StudentD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<ManagerD> TransferStringToManagerD(string response)
    {
        MainData<ManagerD> mainData = JsonConvert.DeserializeObject<MainData<ManagerD>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion
}
