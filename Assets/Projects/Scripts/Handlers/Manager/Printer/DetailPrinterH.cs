using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class DetailPrinterH : MonoBehaviour, IDetailPrinterUpdateHandler
{
    #region -- Implements --

    public string TransferData(PrinterD printer)
    {
        return MainHandler.ToJson<PrinterD>(printer);
    }

    public IEnumerator UpdatePrinterData(PrinterD printer, Action<PrinterD> onSuccess, Action<PrinterD> onFailed)
    {
        string url = $"{_updateURL}/{printer._id}";
        Debug.Log(url);

        string json = TransferData(printer);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                PrinterD updatedPrinter = JsonUtility.FromJson<PrinterD>(request.downloadHandler.text);
                onSuccess?.Invoke(printer);
            }
            else
            {
                Debug.LogError("Error updating printer: " + request.error);
                onFailed?.Invoke(printer);
            }
        }
    }

    #endregion

    #region -- Methods --

    #endregion

    #region -- Fields --

    private readonly string _updateURL = "https://server-wistoria-api.vercel.app/printer/update";

    #endregion
}
