using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;


public class GetPackageH : MonoBehaviour , IGetPackageHandler
{
    #region -- Implements --

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

    public IEnumerator GetPackage (string packagePaper, Action<PackageD> onPackageFound)
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
