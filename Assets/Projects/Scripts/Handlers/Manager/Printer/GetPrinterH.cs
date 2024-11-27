using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;
using Newtonsoft.Json;

public class GetPrinterH : MonoBehaviour, IGetPrinterHandler
{
    #region -- Implements --

    /// <summary>
    /// GET all printer data form server
    /// </summary>
    /// <param name="onPrinterFound">Method will be call when printer information is found</param>
    /// <returns></returns>
    public IEnumerator GetAllPrinter(Action<PrinterD> onPrinterFound, Action<string> onSuccess, Action<string> onFailed)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrl.getAllPrinter))
        {
            yield return request.SendWebRequest();

            MainData<PrinterD> response = TransferJsonToPrinterData(request.downloadHandler.text);

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    onFailed?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    onFailed?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    onFailed?.Invoke(response.Message);
                    break;

                case UnityWebRequest.Result.Success:
                    onSuccess?.Invoke(response.Message);

                    SendData(onPrinterFound, response);
                    break;
            }
        }
    }

    /// <summary>
    /// GET printer by name form server
    /// </summary>
    /// <param name="printerName">Name printer</param>
    /// <param name="onPrinterFound">Method will be call when printer information is found</param>
    /// <returns></returns>
    public IEnumerator SearchPrinterByName(string printerName, Action<PrinterD> onPrinterFound, Action<string> onSuccess, Action<string> onFailed)
    {
        string searchURL = $"{AllUrl.searchPrinterByName}?name={UnityWebRequest.EscapeURL(printerName.Replace(" ", "+"))}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            MainData<PrinterD> response = TransferJsonToPrinterData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                SendData(onPrinterFound, response);
            }
            else
                onFailed?.Invoke(response.Message);
        }
    }

    /// <summary>
    /// GET printer by name form server
    /// </summary>
    /// <param name="printer">Printer data</param>
    /// <param name="onPrinterFound">Method will be call when campus information is found</param>
    /// <returns></returns>
    public IEnumerator SearchCampusByID(string campusId, Action<CampusD> onCampusFound, Action<string> onSuccess, Action<string> onFailed)
    {
        string searchURL = $"{AllUrl.getAllCampus}/search/{campusId}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            MainData<CampusD> response = TransferJsonToCampusData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                Debug.Log(response.Data.Count);
                if (response.Data.Count == 1)
                {
                    CampusD campus = response.Data[0];
                    onCampusFound?.Invoke(campus);
                }
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
    private MainData<PrinterD> TransferJsonToPrinterData(string response)
    {
        MainData<PrinterD> datas = JsonConvert.DeserializeObject<MainData<PrinterD>>(response);
        datas.Initialize();
        return datas;
    }

    private MainData<CampusD> TransferJsonToCampusData(string response)
    {
        MainData <CampusD> datas = JsonConvert.DeserializeObject<MainData<CampusD>>(response);
        datas.Initialize();
        return datas;
    }

    private void SendData(Action<PrinterD> onPrinterFound, MainData<PrinterD> datas)
    {
        if (datas.Data == null)
        {
            Debug.Log(datas.Data);
            return;
        }

        foreach (var data in datas.Data)
        {
            data.ProcessLocateAt();
            onPrinterFound?.Invoke(data);
        }
    }

    #endregion
}