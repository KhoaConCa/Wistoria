using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class UploadPrinterDocH : MonoBehaviour
{
    private const string UploadUrl = "https://server-wistoria-api.vercel.app/printerDoc/create";

    public IEnumerator UploadPrinterDoc(PrinterDocD printerDoc, System.Action<bool, string> onUploadComplete)
    {
        string jsonData = Utilities.MainHandler.ToJson(printerDoc);

        using (UnityWebRequest request = new UnityWebRequest(UploadUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Upload successful: {request.downloadHandler.text}");
                onUploadComplete?.Invoke(true, request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Upload failed: {request.error}");
                onUploadComplete?.Invoke(false, request.error);
            }
        }
    }
}
