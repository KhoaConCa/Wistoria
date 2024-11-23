using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class AddPrinterH : MonoBehaviour, IAddPrinterHandler
{
    #region -- Implements --

    public IEnumerator AddNewPrinter(PrinterD printer, Action<PrinterD> onSuccess)
    {
        string json = TransferDataToJson(printer);
        Debug.Log(json);

        using (UnityWebRequest request = new UnityWebRequest(AllUrl.createPrinter, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(printer);
            }
            else
            {
                Debug.LogError($"Error in call API: {request.error}");
            }
        }

    }

    public IEnumerator GetAllCampus(Action<List<CampusD>> onCampusFound)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrl.getAllCampus))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + request.error);
                    onCampusFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + request.error);
                    onCampusFound?.Invoke(null);
                    break;

                case UnityWebRequest.Result.Success:
                    string jsonResponse = request.downloadHandler.text;
                    List<CampusD> campusD = TransferJsonToData(jsonResponse);
                    onCampusFound?.Invoke(campusD);
                    break;
            }
        }
    }

    #endregion

    #region -- Methods --

    public string TransferDataToJson(PrinterD printerD)
    {

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        return JsonConvert.SerializeObject(printerD, settings);
    }

    public List<CampusD> TransferJsonToData(string response)
    {
        return MainHandler.FromJson<CampusD>(response);
    }

    #endregion

}
