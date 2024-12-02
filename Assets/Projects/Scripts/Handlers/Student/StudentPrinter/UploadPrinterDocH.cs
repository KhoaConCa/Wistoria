using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class UploadPrinterDocH : MonoBehaviour
{
    public IEnumerator UploadPrinterDoc(PrinterDocD printerDoc, Action<string> onSuccess, Action<string> onFaild)
    {
        string json = JsonConvert.SerializeObject(printerDoc); // Serialize JSON
        Debug.Log($"JSON prepared for upload: {json}");

        using (UnityWebRequest request = new UnityWebRequest(AllUrlStudent.createPrinterDoc, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Upload successful: {request.downloadHandler.text}");
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Upload failed: {request.error}");
                Debug.LogError($"Response: {request.downloadHandler.text}");
                onFaild?.Invoke($"Error: {request.error}");
            }
        }
    }

    #region -- Methods --

    public string TransferDataToJson(PrinterDocD printerDoc)
    {
        return MainHandler.ToJson<PrinterDocD>(printerDoc);
    }

    public MainData<PrinterDocD> TransferObjectToData(string response)
    {
        MainData<PrinterDocD> mainData = JsonConvert.DeserializeObject<MainData<PrinterDocD>>(response);
        mainData.Initialize();
        return mainData;
    }

    public MainData<string> TransferStringToData(string response)
    {
        MainData<string> mainData = JsonConvert.DeserializeObject<MainData<string>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion
}
