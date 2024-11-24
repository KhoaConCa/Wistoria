using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;
using System;
using Utilities;

public class UploadPrinterDocH : MonoBehaviour
{
    public IEnumerator UploadPrinterDoc(PrinterDocD printerDoc, Action<bool, string> onComplete)
    {
        string json = MainHandler.ToJson(printerDoc);
        Debug.Log($"JSON prepared for upload: {json}");

        using (UnityWebRequest request = new UnityWebRequest("https://server-wistoria-api.vercel.app/printerDoc/create", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Upload successful");
                onComplete?.Invoke(true, request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Upload failed: {request.error}");
                onComplete?.Invoke(false, request.downloadHandler.text);
            }
        }
    }

}
