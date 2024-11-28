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

    public IEnumerator AddNewPrinter(PrinterD printer, Action<string> onSuccess, Action<string> onFaild)
    {
        string json = TransferDataToJson(printer);

        using (UnityWebRequest request = new UnityWebRequest(AllUrlManager.createPrinter, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<PrinterD> response = TransferJsonToPrinterData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Message);
            else
                onFaild?.Invoke(response.Message);
        }

    }

    public IEnumerator GetAllCampus(Action<List<CampusD>> onCampusFound, Action<string> onSuccess, Action<string> onFaild)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(AllUrlManager.getAllCampus))
        {
            yield return request.SendWebRequest();

            MainData<CampusD> response = TransferJsonToCampusData(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(response.Message);

                onCampusFound?.Invoke(response.Data);
            }
            else
                onFaild?.Invoke(response.Message);
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

    public MainData<CampusD> TransferJsonToCampusData(string response)
    {
        MainData<CampusD> campusData = JsonConvert.DeserializeObject<MainData<CampusD>>(response);
        campusData.Initialize();
        return campusData;
    }

    public MainData<PrinterD> TransferJsonToPrinterData(string response)
    {
        MainData<PrinterD> campusData = JsonConvert.DeserializeObject<MainData<PrinterD>>(response);
        campusData.Initialize();
        return campusData;
    }

    #endregion

}
