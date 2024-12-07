using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

public class UploadPrinterDocH : MonoBehaviour, ICreatePrinterDocHandler
{
    #region -- Methods --

    public IEnumerator CreatePrinterDoc(PrinterDocDStudent printerDoc, Action<string> onSuccess, Action<string> onFaild)
    {
        string json = ProcessingJson.RemoveNullJson(printerDoc);
        Debug.Log($"JSON prepared for upload: {json}");

        using (UnityWebRequest request = new UnityWebRequest(AllUrlStudent.createPrinterDoc, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<PrinterDocDStudent> response = TransferObjectToData<PrinterDocDStudent>(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke($"New Id printer doc: {response.Data[0].Id}");
            else
                onFaild?.Invoke($"Error: {response.Message}");
        }
    }

    public IEnumerator UpdatePaper(StudentD studentD, Action<string> onSuccess, Action<string> onFailed)
    {
        string url = $"{AllUrlStudent.updateStudentById}/{studentD.Id}";
        string json = TransferDataToJson(studentD);

        using (UnityWebRequest request = new UnityWebRequest(url, "PATCH"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            MainData<StudentD> response = TransferObjectToData<StudentD>(request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.Success)
                onSuccess?.Invoke(response.Message);
            else
                onFailed?.Invoke(response.Message);
        }
    }

    public string TransferDataToJson<T>(T data)
    {
        return MainHandler.ToJson<T>(data);
    }

    public MainData<T> TransferObjectToData<T>(string response)
    {
        MainData<T> mainData = JsonConvert.DeserializeObject<MainData<T>>(response);
        mainData.Initialize();
        return mainData;
    }

    #endregion
}
