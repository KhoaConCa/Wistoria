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
        _onPrinterFound = onPrinterFound;

        using (UnityWebRequest request = UnityWebRequest.Get(AllUrl.getAllPrinter))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    onFailed?.Invoke(request.error);
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    onFailed?.Invoke(request.error);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    onFailed?.Invoke(request.error);
                    break;

                case UnityWebRequest.Result.Success:
                    onSuccess?.Invoke(request.result.ToString());
                    string jsonResponse = request.downloadHandler.text;
                    TransferData(jsonResponse);
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
        _onPrinterFound = onPrinterFound;
        string searchURL = $"{AllUrl.searchPrinterByName}?name={UnityWebRequest.EscapeURL(printerName.Replace(" ", "+"))}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.result.ToString());
                string jsonResponse = request.downloadHandler.text;
                TransferData(jsonResponse);
            }
            else
                onFailed?.Invoke(request.error);
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
        string searchURL = $"{AllUrl.getAllCampus}/{campusId}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.result.ToString());
                string jsonResponse = request.downloadHandler.text;
                CampusD campus = JsonConvert.DeserializeObject<CampusD>(jsonResponse);
                onCampusFound?.Invoke(campus);
            }
            else
                onFailed?.Invoke(request.error);
        }
    }

    #endregion

    #region -- Methods --

    /// <summary>
    /// Transfer Json data to List data
    /// </summary>
    /// <param name="response">Json string</param>
    public void TransferData(string response)
    {
        List<PrinterD> printerList;
        try
        {
            printerList = JsonConvert.DeserializeObject<List<PrinterD>>(response);

            foreach (var printer in printerList)
            {
                printer.ProcessLocateAt();

                _onPrinterFound.Invoke(printer);
            }

            Debug.Log("All printers processed.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to parse JSON: {e.Message}");
            return;
        }
    }

    #endregion

    #region -- Fields --

    private Action<PrinterD> _onPrinterFound;

    #endregion
}