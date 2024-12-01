using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;
using Newtonsoft.Json;

public class GetStudentH : MonoBehaviour, IGetStudentHandler
{
    #region -- Implements --

    public IEnumerator GetStudentByID(string id, Action<StudentD> onSuccess, Action<string> onFailed)
    {

        //set URL for system to search the item
        string json = AllUrlStudent.findStudentById + id;

        using (UnityWebRequest request = UnityWebRequest.Get(json))
        {
            yield return request.SendWebRequest();

            MainData<StudentD> response = TransferStringToStudentD(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Data[0]);
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

    #endregion
}
