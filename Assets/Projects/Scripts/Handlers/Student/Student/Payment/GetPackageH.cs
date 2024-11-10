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
public class GetPackageH : MonoBehaviour, IGetPackageHandler
{
    #region -- Implements --

    /// <summary>
    /// Retrieves all packages from the server.
    /// Executes the provided callback for each package found or with null if an error occurs.
    /// </summary>
    /// <param name="onPackageFound">Callback to execute for each package found.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    public IEnumerator GetAllPackage(Action<PackageD> onPackageFound)
    {
        _onPackageFound = onPackageFound;

        using (UnityWebRequest request = UnityWebRequest.Get(_getAllURL))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    _onPackageFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    _onPackageFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;
                    Debug.Log(jsonResponse);
                    TransferData(jsonResponse);
                    break;
            }
        }
    }

    /// <summary>
    /// Retrieves a specific package by paper type from the server.
    /// Executes the provided callback for the found package or with null if an error occurs.
    /// </summary>
    /// <param name="packagePaper">The paper type to search for.</param>
    /// <param name="onPackageFound">Callback to execute for the found package.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    public IEnumerator GetPackage(string packagePaper, Action<PackageD> onPackageFound)
    {
        _onPackageFound = onPackageFound;

        string searchURL = $"{_getURL}?paper={UnityWebRequest.EscapeURL(packagePaper)}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    _onPackageFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    _onPackageFound?.Invoke(null);
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
        List<PackageD> packageList = MainHandler.FromJson<PackageD>(response);

        if (packageList != null && packageList.Count > 0)
        {
            Debug.Log(packageList.Count);
            foreach (var package in packageList)
            {
                _onPackageFound?.Invoke(package);
            }
        }
        else
        {
            Debug.Log("No package found.");
            _onPackageFound?.Invoke(null);
        }
    }

    #endregion

    #region -- Fields --

    private readonly string _getURL = "https://server-wistoria-api.vercel.app/package/search/paper";
    private readonly string _getAllURL = "https://server-wistoria-api.vercel.app/package/";

    private Action<PackageD> _onPackageFound;

    #endregion
}
