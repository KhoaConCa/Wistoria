using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;

public class GetStudentH : MonoBehaviour, IGetStudentHandler
{
    #region -- Implements --

    public IEnumerator GetStudent(string _id)
    {

        //set URL for system to search the item
        string searchURL = $"{_getURL}?_id={UnityWebRequest.EscapeURL(_id)}";

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

/*                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;
                    Debug.Log(jsonResponse);
                    TransferData(jsonResponse);
                    break;*/
            }
        }
    }

    #endregion

    #region -- Methods --

    public void TransferData(string response, string requested_id)
    {
        List<StudentD> studentList = MainHandler.FromJson<StudentD>(response);

        if (studentList != null && studentList.Count > 0)
        {
            var student = studentList.Find(c => c._id == requested_id);
            if (student != null)
            {
                Debug.Log($"Student Name: {student.FirstName} {student.LastName}");
            }
            else
            {
                Debug.Log($"No student match");
            }
        }
        else
        {
            Debug.Log("No student found.");
        }
    }

    #endregion

    #region -- Fields --

    private readonly string _getURL = "https://server-wistoria-api.vercel.app/student/search/id";

    #endregion
}
