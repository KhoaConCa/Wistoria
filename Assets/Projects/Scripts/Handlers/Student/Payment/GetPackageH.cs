using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class GetPackageH : MonoBehaviour, IGetPackageHandler, IGetPackageByPaper
{
    #region -- Implements --

    /// <summary>
    /// Retrieves all packages from the server.
    /// Executes the provided callback for each package found or with null if an error occurs.
    /// </summary>
    /// <param name="onPackageFound">Callback to execute for each package found.</param>
    /// <returns>IEnumerator for coroutine functionality.</returns>
    public IEnumerator GetAllPackage(Action<PackageD> onPackageFound, Action<string> onSuccess, Action<string> onFaild)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlStudent.getPackage))
        {
            yield return request.SendWebRequest();

            MainData<PackageD> response = TransferObjectToData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                SendData(onPackageFound, response);
            }
            else
                onFaild?.Invoke(response.Message);
        }
    }

    public IEnumerator GetPackageByPaper(string paper, Action<PackageJsonD> onPackageFound, Action<string> onSuccess, Action<string> onFailed)
    {
        string searchURL = $"{AllUrlStudent.getPaymentByPaper}?paper={UnityWebRequest.EscapeURL(paper)}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            MainData<PackageJsonD> response = TransferData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                SendPackageData(onPackageFound, response);
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
    public MainData<PackageD> TransferObjectToData(string response)
    {
        MainData<PackageD> mainData = JsonConvert.DeserializeObject<MainData<PackageD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<PackageJsonD> TransferData(string response)
    {
        MainData<PackageJsonD> mainData = JsonConvert.DeserializeObject<MainData<PackageJsonD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<string> TransferStringToData(string response)
    {
        MainData<string> mainData = JsonConvert.DeserializeObject<MainData<string>>(response);
        mainData.Initialize();
        return mainData;
    }

    public void SendData(Action<PackageD> onPackageFound, MainData<PackageD> packageDatas)
    {
        foreach (var itemData in packageDatas.Data)
        {
            onPackageFound?.Invoke(itemData);
        }
    }

    public void SendPackageData(Action<PackageJsonD> onPackageFound, MainData<PackageJsonD> packageDatas)
    {
        foreach (var itemData in packageDatas.Data)
        {
            onPackageFound?.Invoke(itemData);
        }
    }

    #endregion
}
