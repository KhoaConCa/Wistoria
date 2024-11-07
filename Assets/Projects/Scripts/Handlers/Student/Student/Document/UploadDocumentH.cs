using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Text;
using Utilities; // Thêm namespace chứa MainHandler

public class UploadDocumentH : MonoBehaviour, IUploadDocumentHandler
{
    public void UploadDocumentProperties(string filePath)
    {
        StartCoroutine(UploadDocumentPropertiesCoroutine(filePath));
    }

    public IEnumerator UploadDocumentPropertiesCoroutine(string filePath)
    {
        // Lấy thuộc tính của tệp
        FileInfo fileInfo = new FileInfo(filePath);
        string fileName = fileInfo.Name;
        string fileSize = fileInfo.Length.ToString(); // Đảm bảo fileSize là string
        string owner = "671860901e0844975517030e"; // Thay đổi ID của chủ sở hữu nếu cần

        // Tạo đối tượng DocumentData
        DocumentD jsonData = new DocumentD
        {
            NameFile = fileName,
            Size = fileSize,
            Owner = owner
        };

        // Sử dụng MainHandler để chuyển đổi đối tượng thành JSON
        string json = MainHandler.ToJson(jsonData, true);
        Debug.Log("JSON being sent: " + json); // In JSON để kiểm tra

        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest("https://server-wistoria-api.vercel.app/document/create", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            request.timeout = 30;

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Document properties uploaded successfully");
            }
            else
            {
                Debug.LogError("Failed to upload document properties: " + request.error);
            }
        }
    }
}
