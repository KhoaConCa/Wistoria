using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

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
    public IEnumerator GetAllStudentPrinter(Action<StudentPrinterD> onStudentPrinterFound)
    {
        _onStudentPrinterFound = onStudentPrinterFound;

        using (UnityWebRequest request = UnityWebRequest.Get(_getAllURL))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    _onStudentPrinterFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    _onStudentPrinterFound?.Invoke(null);
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

    /// <summary>
    /// Processes the JSON response and invokes the callback for each package found.
    /// </summary>
    /// <param name="response">The JSON response from the server.</param>
    public void TransferData(string response)
    {
        List<StudentPrinterD> studentPrinterList = MainHandler.FromJson<StudentPrinterD>(response);

        if (studentPrinterList != null && studentPrinterList.Count > 0)
        {
            Debug.Log(studentPrinterList.Count);
            foreach (var studentPrinter in studentPrinterList)
            {
                _onStudentPrinterFound?.Invoke(studentPrinter);
            }
        }
        else
        {
            Debug.Log("No package found.");
            _onStudentPrinterFound?.Invoke(null);
        }
    }

    #endregion

    #region -- Fields --

    private readonly string _getAllURL = "https://server-wistoria-api.vercel.app/printer/";

    private Action<StudentPrinterD> _onStudentPrinterFound;

    #endregion
}
