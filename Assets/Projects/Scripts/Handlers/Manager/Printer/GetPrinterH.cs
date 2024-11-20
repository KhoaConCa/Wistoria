using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using Utilities;

public class GetPrinterH : MonoBehaviour, IGetPrinterHandler
{
    #region -- Implements --

    /// <summary>
    /// Transfer Json data to List data
    /// </summary>
    /// <param name="response">Json string</param>
    public void TransferData(string response)
    {
        List<PrinterD> printerList = MainHandler.FromJson<PrinterD>(response);

        if (printerList != null && printerList.Count > 0)
        {
            foreach (var printer in printerList)
            {
                _onPrinterFound?.Invoke(printer);
            }
        }
        else
        {
            Debug.Log("No printer found.");
            _onPrinterFound?.Invoke(null);
        }
    }

    /// <summary>
    /// GET all printer data form server
    /// </summary>
    /// <param name="onPrinterFound">Method will be call when printer information is found</param>
    /// <returns></returns>
    public IEnumerator GetAllPrinter(Action<PrinterD> onPrinterFound)
    {
        _onPrinterFound = onPrinterFound;

        using (UnityWebRequest request = UnityWebRequest.Get(_getAllURL))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    _onPrinterFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    _onPrinterFound?.Invoke(null);
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
    /// GET printer by name form server
    /// </summary>
    /// <param name="printerName">Name printer</param>
    /// <param name="onPrinterFound">Method will be call when printer information is found</param>
    /// <returns></returns>
    public IEnumerator GetPrinter(string printerName, Action<PrinterD> onPrinterFound)
    {
        _onPrinterFound = onPrinterFound;

        string searchURL = $"{_getURL}?name={UnityWebRequest.EscapeURL(printerName)}";

        using (UnityWebRequest request = UnityWebRequest.Get(searchURL))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    _onPrinterFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    _onPrinterFound?.Invoke(null);
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

    #region -- Fields --

    private readonly string _getURL = "https://server-wistoria-api.vercel.app/printer/search/name";
    private readonly string _getAllURL = "https://server-wistoria-api.vercel.app/printer/";

    private Action<PrinterD> _onPrinterFound;

    #endregion
}